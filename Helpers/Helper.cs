using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3M_Firewall.Models;
using Dapper;

namespace _3M_Firewall.Helpers
{
    public class Helper
    {
        private const string db = "Firewall";

        internal void CreateIpRule(RuleModel ruleModel, IpModel ipmodel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("insert into Rule (rulename, ruletype, rulestatus, sessionID) values (@RuleName, @RuleType, @RuleStatus, @SessionID)", ruleModel);

                var p = cnn.Query<int>("select ruleID from Rule where rulename = @RuleName", ruleModel).ToList();

                ipmodel.ruleID = p[0];

                cnn.Execute("insert into Ip (ipaddrname, ruleID) values (@ipaddrname, @RuleID)", ipmodel);
            }
        }

        internal void CreatePortRule(RuleModel ruleModel, PortModel portmodel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("insert into Rule (rulename, ruletype, rulestatus, sessionID) values (@RuleName, @RuleType, @RuleStatus, @SessionID)", ruleModel);

                var p = cnn.Query<int>("select ruleID from Rule where rulename = @RuleName", ruleModel).ToList();

                portmodel.ruleID = p[0];

                cnn.Execute("insert into Port (portname, portnumber, ruleID) values (@PortName, @PortNumber, @RuleID)", portmodel);

            }
        }
                
        internal void CreateAppRule(RuleModel ruleModel, AppModel appModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                 cnn.Execute("insert into Rule (rulename, ruletype, rulestatus, sessionID) values (@RuleName, @RuleType, @RuleStatus, @SessionID)", ruleModel);

                 var p = cnn.Query<int>("select ruleID from Rule where rulename = @RuleName", ruleModel).ToList();

                 appModel.ruleID = p[0];

                 cnn.Execute("insert into App (apppathname, ruleID) values (@AppPathName, @RuleID)", appModel);

            }
        }

        internal void ChangeUserPassword(UserModel userModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("update User set password = @Password where securitykey = @SecurityKey", userModel);
            }
        }

        internal List<RuleModel> GetAllRulesSorted()
        {
            List<RuleModel> output;

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                output = cnn.Query<RuleModel>("select * from Rule order by rulename asc", new DynamicParameters()).ToList();
                return output;
            }
        }

        internal List<RuleModel> GetSessionRules(UserModel userModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                List<RuleModel> output;

                output = cnn.Query<RuleModel>("select * from Rule where sessionID =  @SessionID", userModel).ToList();

                return output;
            }
        }

        internal void StoreSessionID(UserModel userModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                string[] lines = { userModel.SessionID.ToString() };
                File.WriteAllLines("UserFile.txt", lines);
            }
        }

        internal void CreateNewSession(UserModel userModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("insert into User (username, password, securitykey) values (@Username, @Password, @SecurityKey)", userModel);

            }
        }

        internal List<UserModel> GetAllUsers()
        {
            List<UserModel> output;

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                output = cnn.Query<UserModel>("select * from User", new DynamicParameters()).ToList();
                return output;
            }
        }

        internal List<RuleModel> GetAllRules()
        {
            List<RuleModel> output;

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                output = cnn.Query<RuleModel>("select * from Rule", new DynamicParameters()).ToList();
                return output;
            }
        }



        internal void DeleteAllRules()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("delete from Ip");
                cnn.Execute("delete from Port");
                cnn.Execute("delete from App");
                cnn.Execute("delete from Rule");
            }

        }

        internal void DeleteAllSessionRules(UserModel userModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("delete from Ip where rule = (select ruleID from Rule where sessionID = @SessionID)", userModel);
                cnn.Execute("delete from Port where ruleID = (select ruleID from Rule where sessionID = @SessionID)", userModel);
                cnn.Execute("delete from App where ruleID = (select ruleID from Rule where sessionID = @SessionID)", userModel);
                cnn.Execute("delete from Rule where sessionID = @SessionID)", userModel);
            }
        }

        internal void DeleteIpRule(RuleModel ruleModel)
        {

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("delete from Ip where ruleID = (select ruleID from Rule where rulename = @RuleName)", ruleModel);
                cnn.Execute("delete from Rule where rulename = @RuleName", ruleModel);

            }
        }

        internal void DeletePortRule(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("delete from Port where ruleID = (select ruleID from Rule where rulename = @RuleName)", ruleModel);
                cnn.Execute("delete from Rule where rulename = @RuleName", ruleModel);
            }
        }

        internal void DeleteAppRule(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("delete from App where ruleID = (select ruleID from Rule where rulename = @RuleName)", ruleModel);
                cnn.Execute("delete from Rule where rulename = @RuleName", ruleModel);

            }
        }


        internal string GetRule_One(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                var p = cnn.Query<string>("select rulename from Ip where ruleID = (select ruleID from Rule where rulename = @RuleName)", ruleModel).ToList();
                return p[0];
            }
        }

        internal string GetIpRule_One(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                var p = cnn.Query<string>("select ipaddrname from Ip where ruleID = (select ruleID from Rule where rulename = @RuleName)", ruleModel).ToList();
                return p[0];
            }
        }

        internal string GetPortRule_One(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                var p = cnn.Query<string>("select portname from Port where ruleID = (select ruleID from Rule where rulename = @RuleName)", ruleModel).ToList();
                return p[0];
            }
        }

        internal string GetAppRule_One(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                var p = cnn.Query<string>("select apppathname from App where ruleID = (select ruleID from Rule where rulename = @RuleName)", ruleModel).ToList();
                return p[0];
            }
        }


        internal void ChangeRuleName(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("update Rule set rulename = @RuleName where ruleID = @RuleID", ruleModel);
            }
        }

        internal void ChangeIpAddress(IpModel ipModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("update Ip set ipaddrname = @IpAddrName where ruleID = @RuleID", ipModel);
            }
        }

        internal void ChangePort(PortModel portModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("update Port set portname = @PortName, portnumber = @PortNumber where ruleID = @r=RuleID", portModel);
            }
        }

        internal void ChangeAppPath(AppModel appModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("update App set apppathname = @AppPathName where ruleID = @RuleID", appModel);
            }
        }

        internal void ChangeRuleStatus(RuleModel ruleModel)
        { 
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("update Rule set rulestatus = @RuleStatus where ruleID = @RuleID", ruleModel);
            }
        }


        internal void CreateBlockAllTrafficRule(RuleModel ruleModel)

        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("insert into Rule (rulename, ruletype, rulestatus, sessionID) values (@RuleName, @RuleType, @RuleStatus, @SessionID)", ruleModel);
            }
        }

        internal void DeleteBlockAllTrafficRule(RuleModel ruleModel)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString(db)))
            {
                cnn.Execute("delete from Rule where rulename = @RuleName", ruleModel);

            }
        }

        internal int GetLastEntry()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<int>("SELECT count(*) from Rule").ToList();
                return output[0];
            }
        }


        internal void StoreRuleInfo (RuleModel ruleModel)
        {
            string[] lines = { ruleModel.RuleID.ToString(), ruleModel.RuleName, ruleModel.RuleType, ruleModel.RuleStatus };
            File.WriteAllLines("RuleFile.txt", lines);
        }


        public static string LoadConnectionString(string name = "Firewall")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        
    }
}