using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amns.Tessen;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;
using Amns.Rappahanock.Web.Utilities;

namespace Amns.Tessen.Utilities
{
    public class SeminarRegistrationBuilder
    {
        DojoSeminarRegistration registration;
        DojoSeminarRegistrationOptionCollection options;

        public DojoSeminarRegistration Registration { get { return registration; } }
        public DojoSeminarRegistrationOptionCollection Options { get { return options; } }

        public RHCustomer Customer
        {
            get { return registration.Customer; }
            set { registration.Customer = value; }
        }

        public SeminarRegistrationBuilder()
        {
            registration = new DojoSeminarRegistration();
            options = new DojoSeminarRegistrationOptionCollection();
        }

        public SeminarRegistrationBuilder(DojoSeminar seminar) : this()
        {            
            registration.ParentSeminar = seminar;
        }

        public SeminarRegistrationBuilder(int registrationID)
        {
            registration = new DojoSeminarRegistration(registrationID);
            options = new DojoSeminarRegistrationOptionManager().
                GetCollection("ParentRegistrationID=" + registrationID.ToString(),
                string.Empty, DojoSeminarRegistrationOptionFlags.ParentOption);
        }

        public SeminarRegistrationBuilder(DojoSeminarRegistration registration)
        {
            this.registration = registration;
            if (registration.ID != 0)
            {
                options = new DojoSeminarRegistrationOptionManager().
                    GetCollection("ParentRegistrationID=" + registration.ID.ToString(),
                    string.Empty, DojoSeminarRegistrationOptionFlags.ParentOption);
            }
            else
            {
                options = new DojoSeminarRegistrationOptionCollection();
            }
        }

        /// <summary>
        /// Deletes the registraton and options from the database. You must
        /// call RemoveFromCart() before deleting the registration.
        /// </summary>
        public void Delete()
        {
            registration.Delete();

            if (registration.Contact != null)
                registration.Contact.Delete();

            foreach (DojoSeminarRegistrationOption option in options)
            {
                option.Delete();
            }
        }

        /// <summary>
        /// Adds a registration and options to the shopping cart. In order
        /// for the shopping cart and Rappahanock to process correctly, the
        /// registration must have an ID and be saved in the database.
        /// </summary>
        public void AddToCart()
        {
            ShoppingCart cart = ShoppingCart.GetCart();

            if (registration.ID != 0)
            {
                // Add Seminar Registration
                cart.Lines.Add(RHFactory.SalesOrderLine(cart.Order,
                    registration.ParentSeminar.Item, registration));

                // TODO: Speedup with one hit (^^^) here rather than two!

                // Add Options
                foreach (DojoSeminarRegistrationOption option in options)
                {
                    cart.Lines.Add(RHFactory.SalesOrderLine(cart.Order,
                        option.ParentOption.Item, option));


                }

                // Recalc Cart
                cart.Calc();
            }
        }
        
        /// <summary>
        /// Removes registration and its options from the current shopping cart.
        /// The registration must be saved in the database.
        /// </summary>
        public void RemoveFromCart()
        {
            ShoppingCart cart;
            RHSalesOrderLineCollection deleteLines;
            IRHLineExtension lineExtension;
            DojoSeminarRegistration lineRegistration;
            DojoSeminarRegistrationOption lineOption;

            if (registration.ID != 0)
            {
                cart = ShoppingCart.GetCart();
                deleteLines = new RHSalesOrderLineCollection();

                foreach (RHSalesOrderLine line in cart.Lines)
                {
                    // Check for Option
                    try
                    {
                        lineExtension = line.GetLineExtension();

                        if (lineExtension is DojoSeminarRegistrationOption)
                        {
                            lineOption = (DojoSeminarRegistrationOption)
                                lineExtension;

                            if (lineOption.ParentRegistration.ID ==
                                Registration.ID)
                            {
                                deleteLines.Add(line);
                            }
                        }
                        else if (lineExtension is DojoSeminarRegistration)
                        {
                            lineRegistration = (DojoSeminarRegistration)
                                lineExtension;

                            if (lineRegistration.ID ==
                                registration.ID)
                            {
                                deleteLines.Add(line);
                            }
                        }
                    }
                    catch
                    {
                        // The line's extension most likely did not
                        // exist within the database, therefore remove
                        // it from the cart.
                        deleteLines.Add(line);
                    }                    
                }

                // Delete Option Lines
                foreach (RHSalesOrderLine line in deleteLines)
                {
                    cart.Lines.Remove(line);
                }

                // Recalc Cart
                cart.Calc();
            }
        }
    }
}