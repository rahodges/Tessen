using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Amns.GreyFox.Data;
using Amns.GreyFox.People;
using Amns.Rappahanock.Utilities;
using Amns.Tessen.Utilities;

namespace Amns.Tessen.Startup
{
    public class StartupHelper
    {        
        public StartupHelper()
        {
        }

        /// <summary>
        /// Initializes Tessen
        /// </summary>
        public void Initialize()
        {
            // Create Access Control Tables
            new DojoAccessControlGroupManager().CreateTable();
            
            // Create Class and Attendance Tables
            new DojoAttendanceEntryManager().CreateTable();
            new DojoBulkAttendanceEntryManager().CreateTable();
            new DojoClassManager().CreateTable();
            new DojoClassDefinitionManager().CreateTable();
           
            // Create Membership Tables
            new DojoMemberManager().CreateTable();
            new DojoMembershipManager().CreateTable();
            new DojoMemberTypeManager().CreateTable();
            new DojoMembershipTemplateManager().CreateTable();
            new DojoOrganizationManager().CreateTable();

            // Create Ranks, Promotions and Testing Tables
            new DojoPromotionManager().CreateTable();
            new DojoPromotionFlagManager().CreateTable();
            new DojoPromotionStatusManager().CreateTable();
            new DojoRankManager().CreateTable();
            new DojoTestManager().CreateTable();
            new DojoTestListManager().CreateTable();
            new DojoTestListJournalEntryManager().CreateTable();
            new DojoTestListJournalEntryTypeManager().CreateTable();
            new DojoTestListStatusManager().CreateTable();

            // Create Seminar Tables
            new DojoSeminarManager().CreateTable();
            new DojoSeminarOptionManager().CreateTable();
            new DojoSeminarRegistrationManager().CreateTable();
            new DojoSeminarRegistrationOptionManager().CreateTable();
                                    
        }

        /// <summary>
        /// Creates references for Tessen tables.
        /// </summary>
        public void CreateReferences()
        {
            // Create Access Control Tables
            new DojoAccessControlGroupManager().CreateReferences();

            // Create Class and Attendance Tables
            new DojoAttendanceEntryManager().CreateReferences();
            new DojoBulkAttendanceEntryManager().CreateReferences();
            new DojoClassManager().CreateReferences();
            new DojoClassDefinitionManager().CreateReferences();

            // Create Membership Tables
            new DojoMemberManager().CreateReferences();
            new DojoMembershipManager().CreateReferences();
            new DojoMembershipTemplateManager().CreateReferences();
            new DojoOrganizationManager().CreateReferences();

            // Create Ranks, Promotions and Testing Tables
            new DojoPromotionManager().CreateReferences();
            new DojoRankManager().CreateReferences();
            new DojoTestManager().CreateReferences();
            new DojoTestListManager().CreateReferences();
            new DojoTestListJournalEntryManager().CreateReferences();
            new DojoTestListJournalEntryTypeManager().CreateReferences();
            new DojoTestListStatusManager().CreateReferences();

            // Create Seminar Tables
            new DojoSeminarManager().CreateReferences();
            new DojoSeminarOptionManager().CreateReferences();
            new DojoSeminarRegistrationManager().CreateReferences();
            new DojoSeminarRegistrationOptionManager().CreateReferences();

        }


        public string Verify(bool repair)
        {
            System.Text.StringBuilder report = new StringBuilder();
            
            report.Append("Verification Report\r\n");
            report.AppendFormat("for AMNS Tessen - {0:D} UTC \r\n", DateTime.Now.ToUniversalTime());
            report.Append("=Access Control======================================================\r\n");
            report.Append(new DojoAccessControlGroupManager().VerifyTable(repair));
            report.Append("=Attendance==========================================================\r\n");
            report.Append(new DojoAttendanceEntryManager().VerifyTable(repair));
            report.Append(new DojoBulkAttendanceEntryManager().VerifyTable(repair));
            report.Append(new DojoClassManager().VerifyTable(repair));
            report.Append(new DojoClassDefinitionManager().VerifyTable(repair));
            report.Append("=Membership==========================================================\r\n");
            report.Append(new DojoMemberManager().VerifyTable(repair));
            report.Append(new DojoMembershipManager().VerifyTable(repair));
            report.Append(new DojoMembershipTemplateManager().VerifyTable(repair));
            report.Append(new DojoMemberTypeManager().VerifyTable(repair));
            report.Append(new DojoMemberTypeTemplateManager().VerifyTable(repair));
            report.Append(new DojoOrganizationManager().VerifyTable(repair));
            report.Append("=Ranks, Promotions and Testing=======================================\r\n");
            report.Append(new DojoPromotionManager().VerifyTable(repair));
            report.Append(new DojoPromotionFlagManager().VerifyTable(repair));
            report.Append(new DojoPromotionStatusManager().VerifyTable(repair));
            report.Append(new DojoRankManager().VerifyTable(repair));
            report.Append(new DojoTestManager().VerifyTable(repair));
            report.Append(new DojoTestListManager().VerifyTable(repair));
            report.Append(new DojoTestListJournalEntryManager().VerifyTable(repair));
            report.Append(new DojoTestListJournalEntryTypeManager().VerifyTable(repair));
            report.Append(new DojoTestListStatusManager().VerifyTable(repair));
            report.Append("=Seminars============================================================\r\n");
            report.Append(new DojoSeminarManager().VerifyTable(repair));
            report.Append(new DojoSeminarOptionManager().VerifyTable(repair));
            report.Append(new DojoSeminarRegistrationManager().VerifyTable(repair));
            report.Append(new DojoSeminarRegistrationOptionManager().VerifyTable(repair));
            
            return report.ToString();
        }

        public void LoadDefaults()
        {
            DojoAccessControlGroupCollection accessControls;

            DojoAttendanceEntryCollection attendanceEntries;
            DojoBulkAttendanceEntryCollection bulkAttendances;
            DojoClassCollection classes;
            DojoClassDefinitionCollection classDefinitions;

            DojoMemberCollection members;
            DojoMembershipCollection memberships;
            DojoMembershipTemplateCollection membershipTemplates;
            DojoMemberTypeCollection memberTypes;
            DojoMemberTypeTemplateCollection memberTypeTemplates;
            DojoOrganizationCollection organizations;

            DojoPromotionCollection promotions;
            DojoPromotionFlagCollection promotionFlags;
            DojoPromotionStatusCollection promotionStatuses;
            DojoRankCollection ranks;
            DojoTestCollection tests;
            DojoTestListCollection testLists;
            DojoTestListJournalEntryCollection testListJournalEntries;
            DojoTestListJournalEntryTypeCollection testListJournalEntryTypes;
            DojoTestListStatusCollection testListStatuses;

            DojoSeminarCollection seminars;
            DojoSeminarOptionCollection seminarOptions;
            DojoSeminarRegistrationCollection seminarRegistrations;
            DojoSeminarRegistrationOptionCollection seminarRegistrationOptions;
            
            GreyFoxContactCollection locations;
                        
            accessControls = new DojoAccessControlGroupCollection();

            attendanceEntries = new DojoAttendanceEntryCollection();
            bulkAttendances = new DojoBulkAttendanceEntryCollection();
            classes = new DojoClassCollection();
            classDefinitions = new DojoClassDefinitionCollection();

            members = new DojoMemberCollection();
            memberships = new DojoMembershipCollection();
            membershipTemplates = new DojoMembershipTemplateCollection();
            memberTypes = new DojoMemberTypeCollection();
            memberTypeTemplates = new DojoMemberTypeTemplateCollection();
            organizations = new DojoOrganizationCollection();

            promotions = new DojoPromotionCollection();
            promotionFlags = new DojoPromotionFlagCollection();
            promotionStatuses = new DojoPromotionStatusCollection();
            ranks = new DojoRankCollection();
            tests = new DojoTestCollection();
            testLists = new DojoTestListCollection();
            testListJournalEntries = new DojoTestListJournalEntryCollection();
            testListJournalEntryTypes = new DojoTestListJournalEntryTypeCollection();
            testListStatuses = new DojoTestListStatusCollection();

            seminars = new DojoSeminarCollection();
            seminarOptions = new DojoSeminarOptionCollection();
            seminarRegistrations = new DojoSeminarRegistrationCollection();
            seminarRegistrationOptions = new DojoSeminarRegistrationOptionCollection();
                       
            locations = new GreyFoxContactCollection();
            
            organizations = new DojoOrganizationManager().GetCollection(string.Empty, string.Empty, null);
            memberTypes = new DojoMemberTypeManager().GetCollection(string.Empty, string.Empty);
            memberTypeTemplates = new DojoMemberTypeTemplateManager().GetCollection(string.Empty, string.Empty, null);
            ranks = new DojoRankManager().GetCollection(string.Empty, string.Empty, null);
            membershipTemplates = new DojoMembershipTemplateManager().GetCollection(string.Empty, string.Empty, null);

            Dictionary<string, DojoMemberType> memberTypesDictionary = 
                new Dictionary<string,DojoMemberType>();
            foreach(DojoMemberType memberType in memberTypes)
                memberTypesDictionary.Add(memberType.Name, memberType);
            Dictionary<string, DojoMemberTypeTemplate> memberTypeTemplatesDictionary = 
                new Dictionary<string,DojoMemberTypeTemplate>();
            foreach(DojoMemberTypeTemplate typeTemplate in memberTypeTemplates)
                memberTypeTemplatesDictionary.Add(typeTemplate.Name, typeTemplate);
            Dictionary<string, DojoRank> ranksDictionary = 
                new Dictionary<string,DojoRank>();
            foreach(DojoRank rank in ranks)
                ranksDictionary.Add(rank.Name, rank);
            Dictionary<string, DojoMembershipTemplate> membershipTemplatesDictionary =
                new Dictionary<string,DojoMembershipTemplate>();
            foreach(DojoMembershipTemplate template in membershipTemplates)
                membershipTemplatesDictionary.Add(template.Name, template);
                        
            CsvParser.CsvStream csv = 
                CsvParser.StreamParse(Localization.Defaults.Defaults_en_US, false);
            string rowType;
            string[] r = csv.GetNextRow();
            while(r != null)
            {
                rowType = r[0];

                if(rowType == Localization.Defaults.CSVMembershipTemplate)
                {
                        DojoMembershipTemplate template = 
                            TessenFactory.MembershipTemplate(
                            r[1], r[2],
                            bool.Parse(r[3]), bool.Parse(r[4]), bool.Parse(r[5]),
                            decimal.Parse(r[6]), decimal.Parse(r[7]),
                            (DojoMembershipDayType)Enum.Parse(typeof(DojoMembershipDayType), r[8]),
                            (DojoMembershipDayType)Enum.Parse(typeof(DojoMembershipDayType), r[9]),
                            DojoOrganization.NewPlaceHolder(0),
                            1,
                            int.Parse(r[11]), int.Parse(r[12]), decimal.Parse(r[13]), (DojoMembershipDayType)Enum.Parse(typeof(DojoMembershipDayType), r[14]),
                            int.Parse(r[15]), int.Parse(r[16]), decimal.Parse(r[17]), (DojoMembershipDayType)Enum.Parse(typeof(DojoMembershipDayType), r[18]),
                            int.Parse(r[19]), int.Parse(r[20]), decimal.Parse(r[21]), (DojoMembershipDayType)Enum.Parse(typeof(DojoMembershipDayType), r[22]),
                            int.Parse(r[23]), int.Parse(r[24]),
                            int.Parse(r[25]), int.Parse(r[26]),
                            ranksDictionary.ContainsKey(r[27]) ? ranksDictionary[r[27]] : null,
                            ranksDictionary.ContainsKey(r[28]) ? ranksDictionary[r[28]] : null);
                        membershipTemplates.Add(template);
                        membershipTemplatesDictionary.Add(template.Name, template);
                        template.Save();
                }
                else if(rowType == Localization.Defaults.CSVMemberTypeTemplate)
                {
                        DojoMemberTypeTemplate template = TessenFactory.MemberTypeTemplate(
                            r[1], r[2],
                            memberTypeTemplatesDictionary.ContainsKey(r[3].ToString()) ? memberTypeTemplatesDictionary[r[3]] : null,
                            memberTypesDictionary.ContainsKey(r[4]) ? memberTypesDictionary[r[4]] : null,
                            bool.Parse(r[5]), bool.Parse(r[6]), bool.Parse(r[7]), bool.Parse(r[8]),
                            int.Parse(r[9]), int.Parse(r[10]),
                            int.Parse(r[11]), int.Parse(r[12]),
                            ranksDictionary.ContainsKey(r[13]) ? ranksDictionary[r[13]] : null,
                            ranksDictionary.ContainsKey(r[14]) ? ranksDictionary[r[14]] : null,
                            membershipTemplatesDictionary.ContainsKey(r[15]) ? membershipTemplatesDictionary[r[15]] : null,
                            membershipTemplatesDictionary.ContainsKey(r[16]) ? membershipTemplatesDictionary[r[16]] : null,
                            membershipTemplatesDictionary.ContainsKey(r[17]) ? membershipTemplatesDictionary[r[17]] : null,
                            membershipTemplatesDictionary.ContainsKey(r[18]) ? membershipTemplatesDictionary[r[18]] : null,
                            membershipTemplatesDictionary.ContainsKey(r[19]) ? membershipTemplatesDictionary[r[19]] : null);
                        memberTypeTemplates.Add(template);
                        memberTypeTemplatesDictionary.Add(template.Name, template);
                        template.Save();
                }

                r = csv.GetNextRow();
            }            
        }
    }
}
