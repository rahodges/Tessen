/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using Amns.GreyFox;
using Amns.GreyFox.Configuration;
using Amns.GreyFox.People;
using Amns.Tessen;

namespace Amns.Tessen.Utilities
{
	/// <summary>
	/// Summary description for Invoicing.
	/// </summary>
	public class TessenHelper
	{
		const string DEFAULT_MEMBERTYPE_KEY = "Tessen_DefaultMemberTypeID";
		const string DEFAULT_MEMBERRANK_KEY = "Tessen_DefaultMemberRankID";
        const string RAPPAHANOCK_ENABLED_KEY = "Tessen_RappahanockEnabled";

		public static DojoMemberType DefaultMemberType
		{
			get 
			{
                int id = getSetting(DEFAULT_MEMBERTYPE_KEY);

                if (id != -1)
				{
                    return DojoMemberType.NewPlaceHolder(id);
				}
				else
				{
					return null;
				}
			}
			set
			{
                setSetting(DEFAULT_MEMBERTYPE_KEY, value.ID.ToString());
			}
		}

        public static DojoRank DefaultMemberRank
        {
            get
            {
                int id = getSetting(DEFAULT_MEMBERRANK_KEY);

                if (id != -1)
                {
                    return DojoRank.NewPlaceHolder(id);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                setSetting(DEFAULT_MEMBERRANK_KEY, value.ID.ToString());
            }
        }



        private static int getSetting(string key)
        {
            int id;
            string idString;

            idString = GreyFoxSettingManager.GetSetting(key).SettingValue;

            if (idString != string.Empty)
            {
                id = int.Parse(idString);
            }
            else
            {
                id = -1;
            }

            return id;
        }

        private static void setSetting(string key, string value)
        {
            GreyFoxSetting setting =
                GreyFoxSettingManager.GetSetting(key);
            setting.SettingValue = value;
            setting.Save();
        }
	}
}