/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using Amns.GreyFox;
using Amns.GreyFox.Configuration;
using Amns.GreyFox.People;
using Amns.Tessen;
using Amns.Rappahanock;
using Amns.Rappahanock.Utilities;

namespace Amns.Tessen.Utilities
{
    public enum RHCustomerExportMode { ExportAll, ExportParentsOnly }
    public enum RHCustomerSyncMode { UpdateBoth, UpdateTessen, UpdateRappahanock }

    /// <summary>
    /// Summary description for Invoicing.
    /// </summary>
    public class RappahanockHelper
    {
        #region Configuration

        bool rappahanockEnabled;
        RHCustomerExportMode customerExportMode = RHCustomerExportMode.ExportParentsOnly;
        RHCustomerSyncMode customerSyncMode = RHCustomerSyncMode.UpdateBoth;
        RHAccount membershipIncomeAccount = null;
        RHTaxType membershipTax = null;
        RHAccount seminarIncomeAccount = null;
        RHTaxType seminarTax = null;        

        #endregion

        #region Settings

        public void InitSettings()
        {
            GreyFoxSetting tessenSetting;
            GreyFoxSetting setting;

            tessenSetting = GreyFoxSettingManager.GetSetting("Tessen");
            if (tessenSetting == null)
            {
                setting = new GreyFoxSetting();
                setting.Name = "Tessen";
                setting.SettingValue = "Tessen Settings";
                setting.Save();

                tessenSetting = setting;
            }

            setting = GreyFoxSettingManager.GetSetting(tessenSetting, "RappahanockEnabled");
            if(setting == null)
            {
                setting = new GreyFoxSetting();
                setting.Name = "RappahanockEnabled";
                setting.SettingValue = false.ToString();
                setting.Parent = tessenSetting;
                setting.Save();
            }

                setting = new GreyFoxSetting();
                setting.Name = "CustomerExportMode";
                setting.SettingValue = RHCustomerExportMode.ExportParentsOnly.ToString();
                setting.Parent = tessenSetting;
                setting.Save();

                setting = new GreyFoxSetting();
                setting.Name = "CustomerSyncMode";
                setting.SettingValue = RHCustomerSyncMode.UpdateBoth.ToString();
                setting.Parent = tessenSetting;
                setting.Save();

                setting = new GreyFoxSetting();
                setting.Name = "MembershipIncomeAccount";
                setting.SettingValue = "-1";
                setting.Parent = tessenSetting;
                setting.Save();

                setting = new GreyFoxSetting();
                setting.Name = "MembershipTaxCode";
                setting.SettingValue = "-1";
                setting.Parent = tessenSetting;
                setting.Save();

                setting = new GreyFoxSetting();
                setting.Name = "SeminarIncomeAccount";
                setting.SettingValue = "-1";
                setting.Parent = tessenSetting;
                setting.Save();

                setting = new GreyFoxSetting();
                setting.Name = "SeminarTaxCode";
                setting.SettingValue = "-1";
                setting.Parent = tessenSetting;
                setting.Save();
        }

        public void LoadSettings()
        {
            GreyFoxSettingCollection settings =
                GreyFoxSettingManager.GetSettings("Tessen");

            foreach (GreyFoxSetting setting in settings)
            {
                switch (setting.Name)
                {
                    case "RappahanockEnabled":
                        rappahanockEnabled = bool.Parse(setting.SettingValue);
                        break;
                    case "CustomerExportMode":
                        customerExportMode = (RHCustomerExportMode)Enum.Parse(typeof(RHCustomerExportMode),
                            setting.SettingValue);
                        break;
                    case "CustomerSyncMode":
                        customerSyncMode = (RHCustomerSyncMode)Enum.Parse(typeof(RHCustomerSyncMode),
                            setting.SettingValue);
                        break;
                    case "MembershipIncomeAccount":
                        membershipIncomeAccount = RHAccount.NewPlaceHolder(int.Parse(setting.SettingValue));
                        break;
                    case "MembershipTax":
                        membershipTax = RHTaxType.NewPlaceHolder(int.Parse(setting.SettingValue));
                        break;
                    case "SeminarIncomeAccount":
                        seminarIncomeAccount = RHAccount.NewPlaceHolder(int.Parse(setting.SettingValue));
                        break;
                    case "SeminarTax":
                        seminarTax = RHTaxType.NewPlaceHolder(int.Parse(setting.SettingValue));
                        break;

                }
            }
        }

        public void SaveSettings()
        {
            GreyFoxSettingCollection settings =
                GreyFoxSettingManager.GetSettings("Tessen");

            foreach (GreyFoxSetting setting in settings)
            {
                switch (setting.Name)
                {
                    case "RappahanockEnabled":
                        setting.SettingValue = rappahanockEnabled.ToString();
                        setting.Save();
                        break;
                    case "CustomerExportMode":
                        setting.SettingValue = customerExportMode.ToString();
                        setting.Save();
                        break;
                    case "CustomerSyncMode":
                        setting.SettingValue = customerSyncMode.ToString();
                        setting.Save();
                        break;
                    case "MembershipIncomeAccount":
                        setting.SettingValue = membershipIncomeAccount.ID.ToString();
                        setting.Save();
                        break;
                    case "MembershipTax":
                        setting.SettingValue = membershipTax.ID.ToString();
                        setting.Save();
                        break;
                    case "SeminarIncomeAccount":
                        setting.SettingValue = seminarIncomeAccount.ID.ToString();
                        setting.Save();
                        break;
                    case "SeminarTax":
                        setting.SettingValue = seminarTax.ID.ToString();
                        setting.Save();
                        break;
                }                
            }
        }

        #endregion

        /// <summary>
        /// Syncs the Members List to the Customer List. If a Member does not
        /// have an associated Customer, the Customer List is checked for an
        /// existing customer with the same name. If a matching customer is not
        /// found, a new customer is created.
        /// </summary>
        /// <param name="forceSync"></param>
        public void SyncCustomers()
        {
            DojoMemberCollection members;
            RHCustomerCollection customers;

            bool addCustomer;
            bool skipChildren;

            members = new DojoMemberManager().GetCollection(string.Empty, string.Empty,
                DojoMemberFlags.PrivateContact);

            customers = new RHCustomerManager().GetCollection(string.Empty, string.Empty,
                RHCustomerFlags.PrivateContact,
                RHCustomerFlags.BillingContact,
                RHCustomerFlags.ShippingContact);

            skipChildren = customerExportMode == RHCustomerExportMode.ExportParentsOnly;

            foreach (DojoMember member in members)
            {
                if (skipChildren && member.Parent != null)
                {
                    continue;
                }

                // Reset addcustomer flag to keep track of customers
                // that need to be added.
                addCustomer = true;

                foreach (RHCustomer customer in customers)
                {
                    if (member.Customer == null)
                    {
                        if (member.PrivateContact.FirstName == customer.PrivateContact.FirstName &
                            member.PrivateContact.LastName == customer.PrivateContact.LastName &
                            member.PrivateContact.MiddleName == customer.PrivateContact.MiddleName)
                        {
                            member.Customer = customer;
                        }
                    }

                    if (member.Customer.ID == customer.ID)
                    {
                        SyncItem(member, customer);
                        addCustomer = false;
                        break;
                    }
                }

                if (addCustomer)
                {
                    RHCustomer newCustomer = new RHCustomer();

                    newCustomer.PrivateContact = new GreyFoxContact(RHCustomerManager.PrivateContactTable);
                    newCustomer.BillingContact = new GreyFoxContact(RHCustomerManager.BillingContactTable);
                    newCustomer.ShippingContact = new GreyFoxContact(RHCustomerManager.ShippingContactTable);

                    member.PrivateContact.CopyValuesTo(newCustomer.PrivateContact, false);
                    member.PrivateContact.CopyValuesTo(newCustomer.BillingContact, false);
                    member.PrivateContact.CopyValuesTo(newCustomer.ShippingContact, false);

                    newCustomer.PrivateContact.Save();
                    newCustomer.BillingContact.Save();
                    newCustomer.ShippingContact.Save();

                    newCustomer.Save();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member">Member to sync.</param>
        /// <param name="customer">Customer to sync.</param>
        /// <param name="forceSync"></param>
        public void SyncItem(DojoMember member, RHCustomer customer)
        {
            if (member.Customer == null)
            {
                if (member.PrivateContact.FirstName ==
                    customer.PrivateContact.FirstName &
                    member.PrivateContact.LastName ==
                    customer.PrivateContact.LastName &
                    member.PrivateContact.MiddleName ==
                    customer.PrivateContact.MiddleName)
                {
                    member.Customer = customer;

                    if (member.ModifyDate > customer.ModifyDate)
                    {
                        if (customer.PrivateContact == null)
                        {
                            customer.PrivateContact = 
                                new GreyFoxContact(RHCustomerManager.PrivateContactTable);
                        }

                        customer.PrivateContact.DisplayName =
                            member.PrivateContact.ConstructName("L S, F Mi.");

                        customer.PrivateContact.Prefix =
                            member.PrivateContact.Prefix;
                        customer.PrivateContact.FirstName =
                            member.PrivateContact.FirstName;
                        customer.PrivateContact.MiddleName =
                            member.PrivateContact.MiddleName;
                        customer.PrivateContact.LastName =
                            member.PrivateContact.LastName;
                        customer.PrivateContact.Suffix =
                            member.PrivateContact.Suffix;
                        customer.PrivateContact.SuffixCommaEnabled =
                            member.PrivateContact.SuffixCommaEnabled;

                        customer.PrivateContact.HomePhone =
                            member.PrivateContact.HomePhone;
                        customer.PrivateContact.WorkPhone =
                            member.PrivateContact.WorkPhone;
                        customer.PrivateContact.MobilePhone =
                            member.PrivateContact.MobilePhone;
                        customer.PrivateContact.Email1 =
                            member.PrivateContact.Email1;
                        customer.PrivateContact.Url =
                            member.PrivateContact.Url;
                        customer.PrivateContact.BusinessName =
                            member.PrivateContact.BusinessName;
                        customer.PrivateContact.BirthDate =
                            member.PrivateContact.BirthDate;

                        customer.PrivateContact.Address1 =
                            member.PrivateContact.Address1;
                        customer.PrivateContact.Address2 =
                            member.PrivateContact.Address2;
                        customer.PrivateContact.City =
                            member.PrivateContact.City;
                        customer.PrivateContact.StateProvince =
                            member.PrivateContact.StateProvince;
                        customer.PrivateContact.PostalCode =
                            member.PrivateContact.PostalCode;
                        customer.PrivateContact.Country =
                            member.PrivateContact.Country;

                        if (!customer.PrivateContact.IsSynced)
                            customer.IsSynced = false;

                        customer.PrivateContact.Save();
                        customer.Save();
                    }
                    else
                    {
                    }
                }
            }
        }

        public void SyncItem(DojoMembershipTemplate template)
        {
            RHItem item;

            if (template.Item != null)
                item = template.Item;
            else
                item = new RHItem();

            item.Name = template.Name;
            item.Description = template.Description;
            item.IsInventoryItem = false;
            item.IsPurchaseItem = false;
            item.IsSaleItem = true;
            item.IsService = false;
            item.IsWebEnabled = false;
            item.SalesAmount = template.Fee;
            item.SalesAmountIsPercent = false;
            item.SalesDescription = template.Description;
            if (membershipIncomeAccount != null &
                item.SalesIncomeAccount != null)
                item.SalesIncomeAccount = membershipIncomeAccount;
            if (membershipTax != null &
                item.Tax != null)
                item.Tax = membershipTax;
            item.SKU = template.Name;
            item.WebDescription = template.Name;
            item.WebKeywords = "membership";

            item.Save();
        }

        public void SyncItem(DojoSeminar seminar)
        {
            RHItem item;

            if (seminar.Item != null)
                item = seminar.Item;
            else
                item = new RHItem();

            item.Description = seminar.Name;
            item.IsInventoryItem = false;
            item.IsPurchaseItem = false;
            item.IsSaleItem = true;
            item.IsWebEnabled = seminar.RegistrationEnabled;
            item.Name = seminar.Name;
            item.SalesAmount = seminar.FullRegistrationFee;
            item.SalesAmountIsPercent = false;
            item.SalesDescription = seminar.Name;
            if (seminarIncomeAccount != null &
                item.SalesIncomeAccount != null)
                item.SalesIncomeAccount = seminarIncomeAccount;
            if (seminarTax != null &
                item.Tax != null)
                item.Tax = seminarTax;
            item.SKU = seminar.Name;
            item.WebDescription = seminar.Description;
            item.WebKeywords = "seminar";

            item.Save();
        }

        public void CreateInvoice(DojoSeminarRegistration registration,
            RHCustomer customer)
        {
            DojoSeminar seminar = registration.ParentSeminar;

            // Create Invoice and set the duedate according to the seminar
            // registration settings.
            RHInvoice invoice = new RHInvoice();
            invoice.Customer = customer;
            if (seminar.EarlyEndDate > DateTime.Now)
                invoice.DueDate = seminar.EarlyEndDate;
            else if (seminar.EndDate > DateTime.Now)
                invoice.DueDate = seminar.LateStartDate;
            else
                invoice.DueDate = seminar.StartDate;
            invoice.FOB = seminar.Location.BusinessName;
            invoice.ReceivableAccount =
                seminar.Item.SalesIncomeAccount;

            invoice.BillingContact = new GreyFoxContact(RHInvoiceManager.BillingContactTable);
            invoice.BillingContact.Prefix = customer.PrivateContact.Prefix;
            invoice.BillingContact.FirstName = customer.PrivateContact.FirstName;
            invoice.BillingContact.MiddleName = customer.PrivateContact.MiddleName;
            invoice.BillingContact.LastName = customer.PrivateContact.LastName;
            invoice.BillingContact.Suffix = customer.PrivateContact.Suffix;
            invoice.BillingContact.Address1 = customer.PrivateContact.Address1;
            invoice.BillingContact.Address2 = customer.PrivateContact.Address2;
            invoice.BillingContact.City = customer.PrivateContact.City;
            invoice.BillingContact.StateProvince = customer.PrivateContact.StateProvince;
            invoice.BillingContact.PostalCode = customer.PrivateContact.PostalCode;
            invoice.BillingContact.Country = customer.PrivateContact.Country;

            invoice.ShipContact = new GreyFoxContact(RHInvoiceManager.ShipContactTable);
            invoice.ShipContact.Prefix = registration.Contact.Prefix;
            invoice.ShipContact.FirstName = registration.Contact.FirstName;
            invoice.ShipContact.MiddleName = registration.Contact.MiddleName;
            invoice.ShipContact.LastName = registration.Contact.LastName;
            invoice.ShipContact.Suffix = registration.Contact.Suffix;
            invoice.ShipContact.Address1 = registration.Contact.Address1;
            invoice.ShipContact.Address2 = registration.Contact.Address2;
            invoice.ShipContact.City = registration.Contact.City;
            invoice.ShipContact.StateProvince = registration.Contact.StateProvince;
            invoice.ShipContact.PostalCode = registration.Contact.PostalCode;
            invoice.ShipContact.Country = registration.Contact.Country;

            invoice.Subtotal = registration.TotalFee;

            // TODO: Handle Taxes

            invoice.BalanceRemaining = registration.TotalFee;
            invoice.Save();

            // Create Invoice Line
            RHInvoiceLine line = new RHInvoiceLine();
            line.Invoice = invoice;
            line.Description = "Seminar Registration - " +
                seminar.Name + "\r\n";
            line.Item = seminar.Item;
            line.OrderNum = 1;
            line.Quantity = 1;
            line.Amount = registration.TotalFee;
            line.Rate = registration.TotalFee;
            line.Save();

            registration.InvoiceLine = line;
            registration.Save();
        }

        public void PayInvoice(DojoSeminarRegistration registration,
            RHCustomer customer, decimal amount, string reference)
        {
            RHReceivePayment payment = new RHReceivePayment();
            payment.Account = registration.ParentSeminar.Item.SalesIncomeAccount;
            payment.Customer = customer;
            payment.Invoice1 = registration.InvoiceLine.Invoice;
            payment.IsAutomatic = false;
            payment.ReferenceNumber = reference;
            payment.TotalAmount = amount;
            payment.Save();

            registration.InvoiceLine.Invoice.AppliedAmount +=
                payment.TotalAmount;
            registration.InvoiceLine.Invoice.Save();
        }

//        public bool AddToInvoice(InvoiceBuilder builder, DojoMember member,
//            DojoMembershipTemplate template)
//        {
//            RHCustomer customer;
//            GreyFoxContact privateContact;
//            GreyFoxContact billingContact;

//            privateContact = member.PrivateContact;
//            billingContact = member.Customer.BillingContact;
//            customer = member.Customer;

//            RHInvoice invoice = new RHInvoice();
//            invoice.AppliedAmount = 0;
//            invoice.BalanceRemaining = 0;

//            invoice.BillingContact = new GreyFoxContact(RHInvoiceManager.BillingContactTable);
//            invoice.BillingContact.Prefix = billingContact.Prefix;
//            invoice.BillingContact.FirstName = billingContact.FirstName;
//            invoice.BillingContact.MiddleName = billingContact.MiddleName;
//            invoice.BillingContact.LastName = billingContact.LastName;
//            invoice.BillingContact.Suffix = billingContact.Suffix;
//            invoice.BillingContact.Address1 = billingContact.Address1;
//            invoice.BillingContact.Address2 = billingContact.Address2;
//            invoice.BillingContact.City = billingContact.City;
//            invoice.BillingContact.StateProvince = billingContact.StateProvince;
//            invoice.BillingContact.PostalCode = billingContact.PostalCode;
//            invoice.BillingContact.Country = billingContact.Country;
//            invoice.BillingContact.Save();

//            invoice.Customer = member.Customer;
//            invoice.DueDate = member.MembershipPrimary.StartDate;
////          invoice.ReceivableAccount = member.MembershipPrimary.ParentTemplate.Item;

//            invoice.ShipContact = new GreyFoxContact(RHInvoiceManager.ShipContactTable);
//            invoice.ShipContact.Prefix = privateContact.Prefix;
//            invoice.ShipContact.FirstName = privateContact.FirstName;
//            invoice.ShipContact.MiddleName = privateContact.MiddleName;
//            invoice.ShipContact.LastName = privateContact.LastName;
//            invoice.ShipContact.Suffix = privateContact.Suffix;
//            invoice.ShipContact.Address1 = privateContact.Address1;
//            invoice.ShipContact.Address2 = privateContact.Address2;
//            invoice.ShipContact.City = privateContact.City;
//            invoice.ShipContact.StateProvince = privateContact.StateProvince;
//            invoice.ShipContact.PostalCode = privateContact.PostalCode;
//            invoice.ShipContact.Country = privateContact.Country;
//            invoice.ShipContact.Save();

//            invoice.Subtotal = member.MembershipPrimary.Fee;

//            // TODO: Handle Taxes

//            // Create Invoice Line
//            RHInvoiceLine line = new RHInvoiceLine();
//            line.Invoice = invoice;
//            line.Description = "Membership - " +
//                membership.ParentTemplate.Name + "\r\n";
//            line.Item = membership.ParentTemplate.Item;
//            line.OrderNum = 1;
//            line.Quantity = 1;
//            line.Amount = membership.ParentTemplate.Fee;
//            line.Rate = membership.ParentTemplate.Fee;
//            line.Save();

//            invoice.Save();

//            return true;
//        }

        //public void PayInvoice(DojoMembership membership,
        //    RHCustomer customer, decimal amount, string reference)
        //{
        //    RHReceivePayment payment = new RHReceivePayment();
        //    payment.Account = membership.MemberTypeTemplate.Item.SalesIncomeAccount;
        //    payment.Customer = customer;
        //    payment.Invoice1 = membership.InvoiceLine.Invoice;
        //    payment.IsAutomatic = false;
        //    payment.ReferenceNumber = reference;
        //    payment.TotalAmount = amount;
        //    payment.Save();

        //    membership.InvoiceLine.Invoice.AppliedAmount +=
        //        payment.TotalAmount;
        //    membership.InvoiceLine.Invoice.Save();
        //}
    }
}