using SolidCP.EnterpriseServer;
using SolidCP.Portal.UserControls.ScheduleTaskView;
using System;
using System.Web.UI.WebControls;

namespace SolidCP.Portal.ScheduleTaskControls
{
    public partial class LetsEncryptRenewalView : EmptyView
    {
        private readonly static string DaysBeforeParameter;

        private readonly static string NotifyOwnerParameter;

        private readonly static string NotifyEmailParameter;

        protected Label lblDayBefore;

        protected TextBox txtDaysBefore;

        protected Label lblNotifyOwner;

        protected CheckBox cbNotifyOwner;

        protected Label lblNotifyEmail;

        protected TextBox txtNotifyEmail;

        static LetsEncryptRenewalView()
        {
            LetsEncryptRenewalView.DaysBeforeParameter = "DAYS_BEFORE";
            LetsEncryptRenewalView.NotifyOwnerParameter = "NOTIFY_OWNER";
            LetsEncryptRenewalView.NotifyEmailParameter = "NOTIFY_EMAIL";
        }

        public LetsEncryptRenewalView()
        {
        }

        public override ScheduleTaskParameterInfo[] GetParameters()
        {
            ScheduleTaskParameterInfo parameter = base.GetParameter(this.txtDaysBefore, LetsEncryptRenewalView.DaysBeforeParameter);
            ScheduleTaskParameterInfo scheduleTaskParameterInfo = base.GetParameter(this.txtNotifyEmail, LetsEncryptRenewalView.NotifyEmailParameter);
            ScheduleTaskParameterInfo parameter1 = base.GetParameter(this.cbNotifyOwner, LetsEncryptRenewalView.NotifyOwnerParameter);
            return new ScheduleTaskParameterInfo[] { parameter, parameter1, scheduleTaskParameterInfo };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override void SetParameters(ScheduleTaskParameterInfo[] parameters)
        {
            base.SetParameters(parameters);
            base.SetParameter(this.txtDaysBefore, LetsEncryptRenewalView.DaysBeforeParameter);
            base.SetParameter(this.txtNotifyEmail, LetsEncryptRenewalView.NotifyEmailParameter);
            base.SetParameter(this.cbNotifyOwner, LetsEncryptRenewalView.NotifyOwnerParameter);
        }
    }
}