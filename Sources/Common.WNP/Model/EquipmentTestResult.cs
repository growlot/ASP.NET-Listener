//-----------------------------------------------------------------------
// <copyright file="EquipmentTestResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing test results common for all devices
    /// </summary>
    public class EquipmentTestResult : IEquipmentTestResult
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the tester identifier.
        /// </summary>
        /// <value>
        /// The tester identifier.
        /// </value>
        public string TesterId { get; set; }

        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date.
        /// </value>
        public DateTime TestDate { get; set; }

        /// <summary>
        /// Gets or sets the end of test date.
        /// </summary>
        /// <value>
        /// The end of test date.
        /// </value>
        public DateTime TestDateStop { get; set; }

        /// <summary>
        /// Gets or sets the test result step number.
        /// </summary>
        /// <value>
        /// The test result step number.
        /// </value>
        public int StepNumber { get; set; }

        /// <summary>
        /// Gets or sets the primary test reason.
        /// </summary>
        /// <value>
        /// The primary test reason.
        /// </value>
        public string PrimaryTestReason { get; set; }

        /// <summary>
        /// Gets or sets the upper limit.
        /// </summary>
        /// <value>
        /// The upper limit.
        /// </value>
        public decimal UpperLimit { get; set; }

        /// <summary>
        /// Gets or sets the lower limit.
        /// </summary>
        /// <value>
        /// The lower limit.
        /// </value>
        public decimal LowerLimit { get; set; }

        /// <summary>
        /// Gets or sets the custom field1.
        /// </summary>
        /// <value>
        /// The custom field1.
        /// </value>
        public string CustomField1 { get; set; }

        /// <summary>
        /// Gets or sets the custom field2.
        /// </summary>
        /// <value>
        /// The custom field2.
        /// </value>
        public string CustomField2 { get; set; }

        /// <summary>
        /// Gets or sets the custom field3.
        /// </summary>
        /// <value>
        /// The custom field3.
        /// </value>
        public string CustomField3 { get; set; }

        /// <summary>
        /// Gets or sets the custom field4.
        /// </summary>
        /// <value>
        /// The custom field4.
        /// </value>
        public string CustomField4 { get; set; }

        /// <summary>
        /// Gets or sets the custom field5.
        /// </summary>
        /// <value>
        /// The custom field5.
        /// </value>
        public string CustomField5 { get; set; }

        /// <summary>
        /// Gets or sets the custom field6.
        /// </summary>
        /// <value>
        /// The custom field6.
        /// </value>
        public string CustomField6 { get; set; }

        /// <summary>
        /// Gets or sets the custom field7.
        /// </summary>
        /// <value>
        /// The custom field7.
        /// </value>
        public string CustomField7 { get; set; }
    
////CREATE TABLE [wndba].[TCT_TEST_RESULTS](
////    [PERCENT_LOAD] [nvarchar](3) NULL CONSTRAINT [DF__TCT_TEST_R__LOAD__09E968C4]  DEFAULT (NULL),
////    [PERCENT_CHANGE] [float] NULL CONSTRAINT [DF__TCT_TEST___PERCE__0ADD8CFD]  DEFAULT (NULL),
////    [RATIO_RESULT] [float] NULL CONSTRAINT [DF__TCT_TEST___RATIO__0CC5D56F]  DEFAULT (NULL),
////    [SECONDARY_CURRENT] [float] NULL CONSTRAINT [DF__TCT_TEST___SECON__0EAE1DE1]  DEFAULT (NULL),
////    [IN_LIMITS] [nchar](1) NULL CONSTRAINT [DF__TCT_TEST___IN_LI__1372D2FE]  DEFAULT (NULL),
////    [SEC_TEST_REASON] [nvarchar](3) NULL CONSTRAINT [DF__TCT_TEST___SEC_T__155B1B70]  DEFAULT (NULL),
////    [BOARD_NO] [nvarchar](10) NULL CONSTRAINT [DF__TCT_TEST___BOARD__174363E2]  DEFAULT (NULL),
////    [MOD_DATE] [datetime] NULL,
////    [MOD_BY] [nvarchar](32) NULL,
////    [BATCH_NO] [varchar](10) NULL,
////    [shop_cycle] [int] NULL,
////    [results_user08] [varchar](50) NULL,
////    [results_user09] [varchar](50) NULL,
////    [results_user10] [varchar](50) NULL,
////    [PROCESS_TAG] [char](1) NULL,
 
////CREATE TABLE [wndba].[TMETER_TEST_RESULTS](
////--  [SITE] [int] NULL,
////--  [CIRCUIT] [int] NULL,
////--  [CUST_LOAD_TEST] [char](1) NULL,
////--  [STEP_DURATION] [int] NULL,
////--  [ACCURACY_STATUS] [char](1) NULL,

////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [harmonic_configuration] [varchar](250) NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [harmonic_revision] [int] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [standard_sn2] [varchar](20) NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [standard_sn3] [varchar](20) NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [RESULTS_USER08] [varchar](50) NULL DEFAULT (NULL)
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [RESULTS_USER09] [varchar](50) NULL DEFAULT (NULL)
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [RESULTS_USER10] [varchar](50) NULL DEFAULT (NULL)
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [BATCH_NO] [varchar](10) NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [shop_cycle] [int] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [imped_phase_a] [float] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [imped_phase_b] [float] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [imped_phase_c] [float] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [va_phase_a] [float] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [va_phase_b] [float] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [va_phase_c] [float] NULL
////ALTER TABLE [wndba].[TMETER_TEST_RESULTS] ADD [PROCESS_TAG] [char](1) NULL

////CREATE TABLE [wndba].[TPT_TEST_RESULTS](
////    [PERCENT_LOAD] [nvarchar](3) NULL CONSTRAINT [DF__TPT_TEST_R__LOAD__09E968C4]  DEFAULT (NULL),
////    [PERCENT_CHANGE] [float] NULL CONSTRAINT [DF__TPT_TEST___PERCE__0ADD8CFD]  DEFAULT (NULL),
////    [RATIO_RESULT] [float] NULL CONSTRAINT [DF__TPT_TEST___RATIO__0CC5D56F]  DEFAULT (NULL),
////    [SECONDARY_VOLTAGE] [float] NULL CONSTRAINT [DF__TPT_TEST___SECON__0EAE1DE1]  DEFAULT (NULL),
////    [IN_LIMITS] [nchar](1) NULL CONSTRAINT [DF__TPT_TEST___IN_LI__1372D2FE]  DEFAULT (NULL),
////    [SEC_TEST_REASON] [nvarchar](3) NULL CONSTRAINT [DF__TPT_TEST___SEC_T__155B1B70]  DEFAULT (NULL),
////    [BOARD_NO] [nvarchar](10) NULL CONSTRAINT [DF__TPT_TEST___BOARD__174363E2]  DEFAULT (NULL),
////    [MOD_DATE] [datetime] NULL,
////    [MOD_BY] [nvarchar](32) NULL,
////    [BATCH_NO] [varchar](10) NULL,
////    [shop_cycle] [int] NULL,
////    [results_user08] [varchar](50) NULL,
////    [results_user09] [varchar](50) NULL,
////    [results_user10] [varchar](50) NULL,
////    [PROCESS_TAG] [char](1) NULL,
     }
}
