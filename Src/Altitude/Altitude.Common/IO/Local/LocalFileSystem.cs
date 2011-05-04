//
// Copyright (C) 2011 Thomas Mitchell
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//

using System;
using Altitude.IO;

namespace Altitude.IO.Local
{
	public class LocalFileSystem : FileSystem
	{
		private readonly Uri m_basePath;

		public LocalFileSystem(string basePathUri)
			:this(new Uri(basePathUri))
		{
		}

		public LocalFileSystem(Uri basePath)
		{
			m_basePath = basePath;

			if (!System.IO.Directory.Exists(m_basePath.LocalPath))
				System.IO.Directory.CreateDirectory(m_basePath.LocalPath);
		}

		public override Directory CreateDirectory(Path path)
		{
			System.IO.Directory.CreateDirectory(ToLocalPath(path));
			return new Directory(this, path);
		}

		public override bool Exists(Path path)
		{
			return System.IO.File.Exists(ToLocalPath(path));
		}

		public override FileSystemStream OpenFile(Path path, FileMode mode)
		{
			return new LocalFileSystemStream(System.IO.File.Open(ToLocalPath(path), ToLocalFileMode(mode)));
		}

		public override FileSystemStream CreateFile(Path path)
		{
			return new LocalFileSystemStream(System.IO.File.Create(ToLocalPath(path)));
		}

		public override void Delete(Path path)
		{
			System.IO.File.Delete(ToLocalPath(path));
		}

		public override FileInfo GetFileInfo(Path path)
		{
			var fileInfo = new System.IO.FileInfo(ToLocalPath(path));
			return new FileInfo(path, fileInfo.Length);
		}

		public override FileBlock[] GetFileBlocks(FileInfo fileInfo)
		{
			return new []{new FileBlock(0, fileInfo.Length)};
		}

		private string ToLocalPath(Path path)
		{
			return (new Uri(m_basePath, path.Uri)).LocalPath;
		}

		private static System.IO.FileMode ToLocalFileMode(FileMode mode)
		{
			switch (mode)
			{
				case FileMode.Append:
					return System.IO.FileMode.Append;
				case FileMode.CreateNew:
					return System.IO.FileMode.CreateNew;
				case FileMode.Open:
					return System.IO.FileMode.Open;
				case FileMode.OpenOrCreate:
					return System.IO.FileMode.OpenOrCreate;
				default:
					throw new NotSupportedException();
			}
		}
	}
}