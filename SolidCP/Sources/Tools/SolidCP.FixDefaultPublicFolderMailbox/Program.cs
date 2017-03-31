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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SolidCP.FixDefaultPublicFolderMailbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Initialize(ConfigurationManager.AppSettings["LogFile"]);

            bool showHelp = false;
            string param = null;

            if (args.Length==0)
            {
                showHelp = true;
            }
            else
            {
                param = args[0];

                if ((param == "/?") || (param.ToLower() == "/h"))
                    showHelp = true;
            }

            if (showHelp)
            {
                string name = typeof(Log).Assembly.GetName().Name;
                string version = typeof(Log).Assembly.GetName().Version.ToString(3);

                Console.WriteLine("SolidCP Fix default public folder mailbox. " + version);
                Console.WriteLine("Usage :");
                Console.WriteLine(name + " [/All]");
                Console.WriteLine("or");
                Console.WriteLine(name + " [OrganizationId]");
                return;
            }

            Log.WriteApplicationStart();

            if (param.ToLower() == "/all")
                param = null;

            Fix.Start(param);

            Log.WriteApplicationEnd();
        }
    }
}
