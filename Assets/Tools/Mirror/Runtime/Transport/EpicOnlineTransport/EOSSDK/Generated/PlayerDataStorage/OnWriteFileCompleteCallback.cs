// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.PlayerDataStorage
{
	/// <summary>
	/// Callback for when <see cref="PlayerDataStorageInterface.WriteFile" /> completes
	/// </summary>
	public delegate void OnWriteFileCompleteCallback(WriteFileCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(Config.LibraryCallingConvention)]
	internal delegate void OnWriteFileCompleteCallbackInternal(System.IntPtr data);
}