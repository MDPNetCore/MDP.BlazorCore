﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.AspNetCore.Authorization
{
    public class AccessPermission
    {
        // Fields
        private readonly AccessUri _accessUri = null;


        // Consructors
        public AccessPermission(string roleId, List<string> roleScopes, string accessUri)
        {
            #region Contracts

            ArgumentNullException.ThrowIfNullOrEmpty(roleId);
            ArgumentNullException.ThrowIfNull(roleScopes);
            ArgumentNullException.ThrowIfNullOrEmpty(accessUri);

            #endregion

            // Default
            this.RoleId = roleId;
            this.RoleScopes = roleScopes;

            // AccessUri
            _accessUri = new AccessUri(accessUri);
        }


        // Properties
        public string RoleId { get; set; }

        public List<string> RoleScopes { get; set; }

        public string AccessProvider { get { return _accessUri.AccessProvider; } }

        public string AccessType { get { return _accessUri.AccessType; } }

        public string AccessPath { get { return _accessUri.AccessPath; } }

        public string AccessString { get { return _accessUri.AccessString; } }

        internal List<string> AccessPathList { get { return _accessUri.AccessPathList; } }


        // Class
        private class AccessUri
        {
            // Fields
            private readonly Uri _accessUri = null;

            private readonly string _accessString = null;

            private List<string> _accessPathList = null;


            // Constructors
            public AccessUri(string accessUri)
            {
                #region Contracts

                if (string.IsNullOrEmpty(accessUri) == true) throw new ArgumentNullException($"{nameof(accessUri)}=null");

                #endregion

                // AccessUri
                _accessUri = new Uri(accessUri);
                {
                    // AccessUri.AccessProvider
                    if (string.IsNullOrEmpty(this.AccessProvider) == true) throw new InvalidOperationException($"{nameof(this.AccessProvider)}=null");

                    // AccessUri.AccessType
                    if (string.IsNullOrEmpty(this.AccessType) == true) throw new InvalidOperationException($"{nameof(this.AccessType)}=null");

                    // AccessUri.AccessPath
                    if (this.AccessPath == "/") throw new InvalidOperationException($"{nameof(this.AccessPath)}=null");
                    if (this.AccessPath == null) throw new InvalidOperationException($"{nameof(this.AccessPath)}=null");
                    if (this.AccessPath == string.Empty) throw new InvalidOperationException($"{nameof(this.AccessPath)}=null");
                }

                // AccessString
                _accessString = $"{this.AccessType}://{this.AccessProvider}{this.AccessPath}".Replace("\\", "/");
            }


            // Properties
            public string AccessProvider { get { return _accessUri.Host; } }

            public string AccessType { get { return _accessUri.Scheme; } }

            public string AccessPath { get { return _accessUri.AbsolutePath; } }

            public string AccessString { get { return _accessString; } }

            internal List<string> AccessPathList
            {
                get
                {
                    // Create
                    if (_accessPathList == null)
                    {
                        _accessPathList = this.AccessPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                    if (_accessPathList == null) throw new InvalidOperationException($"{nameof(_accessPathList)}=null");

                    // Return
                    return _accessPathList;
                }
            }
        }
    }
}
