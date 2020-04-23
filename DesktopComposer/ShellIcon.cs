using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using IWshRuntimeLibrary;

namespace DesktopComposer
{
    /// <summary>
    /// Summary description for ShellIcon.  Get a small or large Icon with an easy C# function call
    /// that returns a 32x32 or 16x16 System.Drawing.Icon depending on which function you call
    /// either GetSmallIcon(string fileName) or GetLargeIcon(string fileName)
    /// </summary>
    public static class ShellIcon
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Shfileinfo
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        #region ShellIconAPICalls

        public const Int32 MAX_PATH = 260;

        /// <summary>
        /// Receives information used to retrieve a stock Shell icon. This structure is used in a call SHGetStockIconInfo.
        /// </summary>
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        #region APIStructures
        public struct SHSTOCKICONINFO
        {
            /// <summary>
            /// The size of this structure, in bytes.
            /// </summary>
            public UInt32 cbSize;
            /// <summary>
            /// When SHGetStockIconInfo is called with the SHGSI_ICON flag, this member receives a handle to the icon.
            /// </summary>
            public IntPtr hIcon;
            /// <summary>
            /// When SHGetStockIconInfo is called with the SHGSI_SYSICONINDEX flag, this member receives the index of the image in the system icon cache.
            /// </summary>
            public Int32 iSysIconIndex;
            /// <summary>
            /// When SHGetStockIconInfo is called with the SHGSI_ICONLOCATION flag, this member receives the index of the icon in the resource whose path is received in szPath.
            /// </summary>
            public Int32 iIcon;
            /// <summary>
            /// When SHGetStockIconInfo is called with the SHGSI_ICONLOCATION flag, this member receives the path of the resource that contains the icon. The index of the icon within the resource is received in iIcon.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szPath;
        }

        /// <summary>
        /// UInt Enumeration with Flags that specify which information is requested.
        /// </summary>
        [Flags()]
        public enum SHGSI : UInt32
        {

            /// <summary>
            /// The szPath and iIcon members of the SHSTOCKICONINFO structure receive the path and icon index of the requested icon, in a format suitable for passing to the ExtractIcon function. The numerical value of this flag is zero, so you always get the icon location regardless of other flags.
            /// </summary>
            SHGSI_ICONLOCATION = 0,

            /// <summary>
            /// The hIcon member of the SHSTOCKICONINFO structure receives a handle to the specified icon.
            /// </summary>
            SHGSI_ICON = 0x100,

            /// <summary>
            /// The iSysImageImage member of the SHSTOCKICONINFO structure receives the index of the specified icon in the system imagelist.
            /// </summary>
            SHGSI_SYSICONINDEX = 0x4000,

            /// <summary>
            /// Modifies the SHGSI_ICON value by causing the function to add the link overlay to the file's icon.
            /// </summary>
            SHGSI_LINKOVERLAY = 0x8000,

            /// <summary>
            /// Modifies the SHGSI_ICON value by causing the function to blend the icon with the system highlight color.
            /// </summary>
            SHGSI_SELECTED = 0x10000,

            /// <summary>
            /// Modifies the SHGSI_ICON value by causing the function to retrieve the large version of the icon, as specified by the SM_CXICON and SM_CYICON system metrics.
            /// </summary>
            SHGSI_LARGEICON = 0x0,

            /// <summary>
            /// Modifies the SHGSI_ICON value by causing the function to retrieve the small version of the icon, as specified by the SM_CXSMICON and SM_CYSMICON system metrics.
            /// </summary>
            SHGSI_SMALLICON = 0x1,

            /// <summary>
            /// Modifies the SHGSI_LARGEICON or SHGSI_SMALLICON values by causing the function to retrieve the Shell-sized icons rather than the sizes specified by the system metrics.
            /// </summary>
            SHGSI_SHELLICONSIZE = 0x4

        }

        /// <summary>
        /// Used by SHGetStockIconInfo to identify which stock system icon to retrieve.
        /// </summary>
        /// <remarks>SIID_INVALID, with a value of -1, indicates an invalid SHSTOCKICONID value.</remarks>
        public enum SHSTOCKICONID : UInt32
        {
            /// <summary>
            /// Document of a type with no associated application.
            /// </summary>
            SIID_DOCNOASSOC = 0,
            /// <summary>
            /// Document of a type with an associated application.
            /// </summary>
            SIID_DOCASSOC = 1,
            /// <summary>
            /// Generic application with no custom icon.
            /// </summary>
            SIID_APPLICATION = 2,
            /// <summary>
            /// Folder (generic, unspecified state).
            /// </summary>
            SIID_FOLDER = 3,
            /// <summary>
            /// Folder (open).
            /// </summary>
            SIID_FOLDEROPEN = 4,
            /// <summary>
            /// 5.25-inch disk drive.
            /// </summary>
            SIID_DRIVE525 = 5,
            /// <summary>
            /// 3.5-inch disk drive.
            /// </summary>
            SIID_DRIVE35 = 6,
            /// <summary>
            /// Removable drive.
            /// </summary>
            SIID_DRIVEREMOVE = 7,
            /// <summary>
            /// Fixed drive (hard disk).
            /// </summary>
            SIID_DRIVEFIXED = 8,
            /// <summary>
            /// Network drive (connected).
            /// </summary>
            SIID_DRIVENET = 9,
            /// <summary>
            /// Network drive (disconnected).
            /// </summary>
            SIID_DRIVENETDISABLED = 10,
            /// <summary>
            /// CD drive.
            /// </summary>
            SIID_DRIVECD = 11,
            /// <summary>
            /// RAM disk drive.
            /// </summary>
            SIID_DRIVERAM = 12,
            /// <summary>
            /// The entire network.
            /// </summary>
            SIID_WORLD = 13,
            /// <summary>
            /// A computer on the network.
            /// </summary>
            SIID_SERVER = 15,
            /// <summary>
            /// A local printer or print destination.
            /// </summary>
            SIID_PRINTER = 16,
            /// <summary>
            /// The Network virtual folder (FOLDERID_NetworkFolder/CSIDL_NETWORK).
            /// </summary>
            SIID_MYNETWORK = 17,
            /// <summary>
            /// The Search feature.
            /// </summary>
            SIID_FIND = 22,
            /// <summary>
            /// The Help and Support feature.
            /// </summary>
            SIID_HELP = 23,

            // OVERLAYS...

            /// <summary>
            /// Overlay for a shared item.
            /// </summary>
            SIID_SHARE = 28,
            /// <summary>
            /// Overlay for a shortcut.
            /// </summary>
            SIID_LINK = 29,
            /// <summary>
            /// Overlay for items that are expected to be slow to access.
            /// </summary>
            SIID_SLOWFILE = 30,

            // MORE ICONS...

            /// <summary>
            /// The Recycle Bin (empty).
            /// </summary>
            SIID_RECYCLER = 31,
            /// <summary>
            /// The Recycle Bin (not empty).
            /// </summary>
            SIID_RECYCLERFULL = 32,
            /// <summary>
            /// Audio CD media.
            /// </summary>
            SIID_MEDIACDAUDIO = 40,
            /// <summary>
            /// Security lock.
            /// </summary>
            SIID_LOCK = 47,
            /// <summary>
            /// A virtual folder that contains the results of a search.
            /// </summary>
            SIID_AUTOLIST = 49,
            /// <summary>
            /// A network printer.
            /// </summary>
            SIID_PRINTERNET = 50,
            /// <summary>
            /// A server shared on a network.
            /// </summary>
            SIID_SERVERSHARE = 51,
            /// <summary>
            /// A local fax printer.
            /// </summary>
            SIID_PRINTERFAX = 52,
            /// <summary>
            /// A network fax printer.
            /// </summary>
            SIID_PRINTERFAXNET = 53,
            /// <summary>
            /// A file that receives the output of a Print to file operation.
            /// </summary>
            SIID_PRINTERFILE = 54,
            /// <summary>
            /// A category that results from a Stack by command to organize the contents of a folder.
            /// </summary>
            SIID_STACK = 55,
            /// <summary>
            /// Super Video CD (SVCD) media.
            /// </summary>
            SIID_MEDIASVCD = 56,
            /// <summary>
            /// A folder that contains only subfolders as child items.
            /// </summary>
            SIID_STUFFEDFOLDER = 57,
            /// <summary>
            /// Unknown drive type.
            /// </summary>
            SIID_DRIVEUNKNOWN = 58,
            /// <summary>
            /// DVD drive.
            /// </summary>
            SIID_DRIVEDVD = 59,
            /// <summary>
            /// DVD media.
            /// </summary>
            SIID_MEDIADVD = 60,
            /// <summary>
            /// DVD-RAM media.
            /// </summary>
            SIID_MEDIADVDRAM = 61,
            /// <summary>
            /// DVD-RW media.
            /// </summary>
            SIID_MEDIADVDRW = 62,
            /// <summary>
            /// DVD-R media.
            /// </summary>
            SIID_MEDIADVDR = 63,
            /// <summary>
            /// DVD-ROM media.
            /// </summary>
            SIID_MEDIADVDROM = 64,
            /// <summary>
            /// CD+ (enhanced audio CD) media.
            /// </summary>
            SIID_MEDIACDAUDIOPLUS = 65,
            /// <summary>
            /// CD-RW media.
            /// </summary>
            SIID_MEDIACDRW = 66,
            /// <summary>
            /// CD-R media.
            /// </summary>
            SIID_MEDIACDR = 67,
            /// <summary>
            /// A writeable CD in the process of being burned.
            /// </summary>
            SIID_MEDIACDBURN = 68,
            /// <summary>
            /// Blank writable CD media.
            /// </summary>
            SIID_MEDIABLANKCD = 69,
            /// <summary>
            /// CD-ROM media.
            /// </summary>
            SIID_MEDIACDROM = 70,
            /// <summary>
            /// An audio file.
            /// </summary>
            SIID_AUDIOFILES = 71,
            /// <summary>
            /// An image file.
            /// </summary>
            SIID_IMAGEFILES = 72,
            /// <summary>
            /// A video file.
            /// </summary>
            SIID_VIDEOFILES = 73,
            /// <summary>
            /// A mixed (media) file.
            /// </summary>
            SIID_MIXEDFILES = 74,


            /// <summary>
            /// Folder back. Represents the background Fold of a Folder.
            /// </summary>
            SIID_FOLDERBACK = 75,
            /// <summary>
            /// Folder front. Represents the foreground Fold of a Folder.
            /// </summary>
            SIID_FOLDERFRONT = 76,
            /// <summary>
            /// Security shield.
            /// </summary>
            /// <remarks>Use for UAC prompts only. This Icon doesn't work on all purposes.</remarks>
            SIID_SHIELD = 77,
            /// <summary>
            /// Warning (Exclamation mark).
            /// </summary>
            SIID_WARNING = 78,
            /// <summary>
            /// Informational (Info).
            /// </summary>
            SIID_INFO = 79,
            /// <summary>
            /// Error (X).
            /// </summary>
            SIID_ERROR = 80,
            /// <summary>
            /// Key.
            /// </summary>
            SIID_KEY = 81,
            /// <summary>
            /// Software.
            /// </summary>
            SIID_SOFTWARE = 82,
            /// <summary>
            /// A UI item, such as a button, that issues a rename command.
            /// </summary>
            SIID_RENAME = 83,
            /// <summary>
            /// A UI item, such as a button, that issues a delete command.
            /// </summary>
            SIID_DELETE = 84,
            /// <summary>
            /// Audio DVD media.
            /// </summary>
            SIID_MEDIAAUDIODVD = 85,
            /// <summary>
            /// Movie DVD media.
            /// </summary>
            SIID_MEDIAMOVIEDVD = 86,
            /// <summary>
            /// Enhanced CD media.
            /// </summary>
            SIID_MEDIAENHANCEDCD = 87,
            /// <summary>
            /// Enhanced DVD media.
            /// </summary>
            SIID_MEDIAENHANCEDDVD = 88,
            /// <summary>
            /// Enhanced DVD media.
            /// </summary>
            SIID_MEDIAHDDVD = 89,
            /// <summary>
            /// High definition DVD media in the Blu-ray Disc™ format.
            /// </summary>
            SIID_MEDIABLURAY = 90,
            /// <summary>
            /// Video CD (VCD) media.
            /// </summary>
            SIID_MEDIAVCD = 91,
            /// <summary>
            /// DVD+R media.
            /// </summary>
            SIID_MEDIADVDPLUSR = 92,
            /// <summary>
            /// DVD+RW media.
            /// </summary>
            SIID_MEDIADVDPLUSRW = 93,
            /// <summary>
            /// A desktop computer.
            /// </summary>
            SIID_DESKTOPPC = 94,
            /// <summary>
            /// A mobile computer (laptop).
            /// </summary>
            SIID_MOBILEPC = 95,
            /// <summary>
            /// The User Accounts Control Panel item.
            /// </summary>
            SIID_USERS = 96,
            /// <summary>
            /// Smart media.
            /// </summary>
            SIID_MEDIASMARTMEDIA = 97,
            /// <summary>
            /// CompactFlash media.
            /// </summary>
            SIID_MEDIACOMPACTFLASH = 98,
            /// <summary>
            /// A cell phone.
            /// </summary>
            SIID_DEVICECELLPHONE = 99,
            /// <summary>
            /// A digital camera.
            /// </summary>
            SIID_DEVICECAMERA = 100,
            /// <summary>
            /// A digital video camera.
            /// </summary>
            SIID_DEVICEVIDEOCAMERA = 101,
            /// <summary>
            /// An audio player.
            /// </summary>
            SIID_DEVICEAUDIOPLAYER = 102,
            /// <summary>
            /// Connect to network.
            /// </summary>
            SIID_NETWORKCONNECT = 103,
            /// <summary>
            /// The Network and Internet Control Panel item.
            /// </summary>
            SIID_INTERNET = 104,
            /// <summary>
            /// A compressed file with a .zip file name extension.
            /// </summary>
            SIID_ZIPFILE = 105,
            /// <summary>
            /// The Additional Options Control Panel item.
            /// </summary>
            SIID_SETTINGS = 106,
            /// <summary>
            /// Windows Vista with Service Pack 1 (SP1) and later. High definition DVD drive (any type - HD DVD-ROM, HD DVD-R, HD-DVD-RAM) that uses the HD DVD format.
            /// </summary>
            SIID_DRIVEHDDVD = 132,
            /// <summary>
            /// Windows Vista with SP1 and later. High definition DVD drive (any type - BD-ROM, BD-R, BD-RE) that uses the Blu-ray Disc format.
            /// </summary>
            SIID_DRIVEBD = 133,
            /// <summary>
            /// Windows Vista with SP1 and later. High definition DVD-ROM media in the HD DVD-ROM format.
            /// </summary>
            SIID_MEDIAHDDVDROM = 134,
            /// <summary>
            /// Windows Vista with SP1 and later. High definition DVD-R media in the HD DVD-R format.
            /// </summary>
            SIID_MEDIAHDDVDR = 135,
            /// <summary>
            /// Windows Vista with SP1 and later. High definition DVD-RAM media in the HD DVD-RAM format.
            /// </summary>
            SIID_MEDIAHDDVDRAM = 136,
            /// <summary>
            /// Windows Vista with SP1 and later. High definition DVD-ROM media in the Blu-ray Disc BD-ROM format.
            /// </summary>
            SIID_MEDIABDROM = 137,
            /// <summary>
            /// Windows Vista with SP1 and later. High definition write-once media in the Blu-ray Disc BD-R format.
            /// </summary>
            SIID_MEDIABDR = 138,
            /// <summary>
            /// Windows Vista with SP1 and later. High definition read/write media in the Blu-ray Disc BD-RE format.
            /// </summary>
            SIID_MEDIABDRE = 139,
            /// <summary>
            /// Windows Vista with SP1 and later. A cluster disk array.
            /// </summary>
            SIID_CLUSTEREDDRIVE = 140,

            /// <summary>
            /// The highest valid value in the enumeration. Values over 160 are Windows 7-only icons.
            /// </summary>
            SIID_MAX_ICONS = 175
        }
        #endregion

        /// <summary>
        /// Retrieves information about a stock icon.
        /// </summary>
        /// <param name="siid">One of the values from the SHSTOCKICONID enumeration that specifies which icon should be retrieved.</param>
        /// <param name="uFlags">A combination of zero or more of the following flags that specify which information is requested.</param>
        /// <param name="psii">A pointer to a SHSTOCKICONINFO structure. When this function is called, the cbSize member of this structure needs to be set to the size of the SHSTOCKICONINFO structure. When this function returns, contains a pointer to a SHSTOCKICONINFO structure that contains the requested information.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        /// <remarks>If this function returns an icon handle in the hIcon member of the SHSTOCKICONINFO structure pointed to by psii, you are responsible for freeing the icon with DestroyIcon when you no longer need it.</remarks>
        [DllImport("Shell32.dll", SetLastError = false)]
        private static extern Int32 SHGetStockIconInfo(SHSTOCKICONID siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);


        /// <summary>
        /// Gets the Pointer to the (stock) Icon associated to the specified ID.
        /// </summary>
        /// <param name="StockIconID">Icon ID among the defined Stock ones.</param>
        /// <returns>The Pointer to the retrieved Icon. If no Icon were found, an empty Pointer is returned.</returns>
        private static IntPtr GetShellIconPointer(SHSTOCKICONID StockIconID, SHGSI IconOptions)
        {
            SHSTOCKICONINFO StkIconInfo = new SHSTOCKICONINFO();
            StkIconInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(SHSTOCKICONINFO)));

            if (SHGetStockIconInfo(StockIconID, IconOptions, ref StkIconInfo) == 0)
            {
                return StkIconInfo.hIcon;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Gets the (stock) Icon associated to the specified ID.
        /// </summary>
        /// <param name="StockIconID">Icon ID among the defined Stock ones.</param>
        /// <returns>The (stock) Icon. If no Icon were found, Null is returned.</returns>
        /// <remarks>WARNING ! Caller is responsible of calling Dispose() on the returned Icon.</remarks>
        public static Icon GetSystemIcon(SHSTOCKICONID stockIconID, SHGSI iconOptions)
        {
            IntPtr iconPointer = GetShellIconPointer(stockIconID, iconOptions);

            if (iconPointer != IntPtr.Zero)
            {
                Icon actualIcon = Icon.FromHandle(iconPointer);
                Icon iconCopy = (System.Drawing.Icon)actualIcon.Clone();

                actualIcon.Dispose();
                Win32.DestroyIcon(iconPointer);
                return iconCopy;
            }
            else
            {
                return null;
            }
        }
        #endregion
        class Win32
        {
            public const uint ShgfiIcon = 0x100;
            public const uint ShgfiLargeicon = 0x0; // 'Large icon
            public const uint ShgfiSmallicon = 0x1; // 'Small icon            

            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref Shfileinfo psfi, uint cbSizeFileInfo, uint uFlags);

            [DllImport("User32.dll")]
            public static extern int DestroyIcon(IntPtr hIcon);

            [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

            [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath, out ushort lpiIcon);
        }

        public static Icon GetAssociatedIcon(string fileName){
            ushort uicon;
            StringBuilder strB = new StringBuilder(260); // Allocate MAX_PATH chars
            strB.Append(fileName);
            IntPtr handle = Win32.ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
            Icon ico = (Icon)Icon.FromHandle(handle).Clone();
            Win32.DestroyIcon(handle);
            return ico;
        }

        public static Icon GetSmallIcon(string fileName)
        {
            return GetIcon(fileName, Win32.ShgfiSmallicon);
        }

        public static Icon GetLargeIcon(string fileName)
        {
            return GetIcon(fileName, Win32.ShgfiLargeicon);
        }

        private static Icon GetIcon(string fileName, uint flags)
        {
            var shinfo = new Shfileinfo();
            Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.ShgfiIcon | flags);

            var icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
            Win32.DestroyIcon(shinfo.hIcon);
            return icon;
        }

        public static Icon IconExtract(string Filename, int IconIndex=0, int IconSize=0)
        {
            IntPtr large;
            IntPtr small;
            Icon IconRet;
            Win32.ExtractIconEx(Filename, IconIndex, out large, out small, 1);
            try
            {
                if (IconSize==0)
                    IconRet = (Icon)Icon.FromHandle(small).Clone();
                else
                    IconRet = (Icon)Icon.FromHandle(large).Clone();
            }
            catch
            {
                IconRet = null;
            }
            Win32.DestroyIcon(large);
            Win32.DestroyIcon(small);

            return IconRet;
        }        

    }
}
