using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DesktopComposer
{    
        public static class MsiShortcutParser

        {

            /*

            UINT MsiGetShortcutTarget(

                LPCTSTR szShortcutTarget,

                LPTSTR szProductCode,

                LPTSTR szFeatureId,

                LPTSTR szComponentCode

            );

            */

            [DllImport("msi.dll", CharSet = CharSet.Auto)]

            static extern int MsiGetShortcutTarget(string targetFile, StringBuilder productCode, StringBuilder featureID, StringBuilder componentCode);



            public enum InstallState

            {

                NotUsed = -7,

                BadConfig = -6,

                Incomplete = -5,

                SourceAbsent = -4,

                MoreData = -3,

                InvalidArg = -2,

                Unknown = -1,

                Broken = 0,

                Advertised = 1,

                Removed = 1,

                Absent = 2,

                Local = 3,

                Source = 4,

                Default = 5

            }



            public const int MaxFeatureLength = 38;

            public const int MaxGuidLength = 38;

            public const int MaxPathLength = 1024;



            /*

            INSTALLSTATE MsiGetComponentPath(

              LPCTSTR szProduct,

              LPCTSTR szComponent,

              LPTSTR lpPathBuf,

              DWORD* pcchBuf

            );

            */

            [DllImport("msi.dll", CharSet = CharSet.Auto)]

            static extern InstallState MsiGetComponentPath(string productCode, string componentCode, StringBuilder componentPath, ref int componentPathBufferSize);



            public static string ParseShortcut(string file)

            {

                StringBuilder product = new StringBuilder(MaxGuidLength + 1);

                StringBuilder feature = new StringBuilder(MaxFeatureLength + 1);

                StringBuilder component = new StringBuilder(MaxGuidLength + 1);



                MsiGetShortcutTarget(file, product, feature, component);



                int pathLength = MaxPathLength;

                StringBuilder path = new StringBuilder(pathLength);



                InstallState installState = MsiGetComponentPath(product.ToString(), component.ToString(), path, ref pathLength);

                if (installState == InstallState.Local)

                {

                    return path.ToString();

                }

                else

                {
                    return null;
                }

            }

        }
    
}
