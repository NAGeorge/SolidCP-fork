// Copyright (c) 2016, SolidCP
// SolidCP is distributed under the Creative Commons Share-alike license
// 
// SolidCP is a fork of WebsitePanel:
// Copyright (c) 2015, Outercurve Foundation.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of  the  Outercurve Foundation  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using SolidCP.Providers;
using SolidCP.Providers.Common;
using SolidCP.Providers.HostedSolution;
using SolidCP.Providers.ResultObjects;
using SolidCP.Providers.SfB;
using SolidCP.EnterpriseServer.Code.HostedSolution;


namespace SolidCP.EnterpriseServer.Code.HostedSolution
{
    public class SfBControllerAsync
    {
        private int sfbServiceId;
        private int organizationServiceId;

        public int SfBServiceId
        {
            get { return this.sfbServiceId; }
            set { this.sfbServiceId = value; }
        }

        public int OrganizationServiceId
        {
            get { return this.organizationServiceId; }
            set { this.organizationServiceId = value; }
        }


        public void Enable_CsComputerAsync()
        {
            // start asynchronously
            Thread t = new Thread(new ThreadStart(Enable_CsComputer));
            t.Start();
        }

        private void Enable_CsComputer()
        {
            int[] sfbServiceIds;

            SfBController.GetSfBServices(sfbServiceId, out sfbServiceIds);

            foreach (int id in sfbServiceIds)
            {
                SfBServer sfb = null;
                try
                {
                    sfb = SfBController.GetSfBServer(id, organizationServiceId);
                    if (sfb != null)
                    {
                        sfb.ReloadConfiguration();
                    }
                }
                catch (Exception exe)
                {
                    TaskManager.WriteError(exe);
                    continue;
                }
            }


        }


    }
}
