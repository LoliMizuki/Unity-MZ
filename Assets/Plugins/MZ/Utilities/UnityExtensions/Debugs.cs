#define DEBUG

using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;

public static partial class MZ {

    public class Debugs {
    
        public static string GetTraceStackName(int skipFrames) {
            StackTrace stackTrace = new System.Diagnostics.StackTrace(skipFrames);
            int frameIndex = 0;
            StackFrame stackFrame = stackTrace.GetFrame(frameIndex);

            return stackFrame.GetMethod().DeclaringType.FullName + "." + stackFrame.GetMethod().Name + "(): ";
        }

        public static void Trace(int skipFrames) {
            UnityEngine.Debug.Log(GetTraceStackName(skipFrames));
        }

        public static void Trace() {
            // caller -> Trace() --> Trace(skipframes)
            Trace(3);
        }

        public static void Log(string message) {
            if (Application.platform != RuntimePlatform.OSXEditor) return;
            
            LogWithSkipFrames(3, message);
        }

        public static void Log(string format, params object[] args) {
			if (Application.platform != RuntimePlatform.OSXEditor) return;
			
            string message = string.Format(format, args);
            LogWithSkipFrames(3, message);
        }

        public static void LogBreak(string message) {
			if (Application.platform != RuntimePlatform.OSXEditor) return;
        
            LogWithSkipFrames(3, message);
            UnityEngine.Debug.Break();
        }

        public static bool Alert(bool condition, string message) {
			if (Application.platform != RuntimePlatform.OSXEditor) return false;
        
            if (!condition) {
                string fullMsg = GetTraceStackName(2) + message;
                UnityEngine.Debug.Log(fullMsg);
            }

            return condition;
        }

        public static void Assert(bool condition, string message) {
            AssertWithSkipFrames(3, condition, message);
        }

        public static void Assert(bool condition, string format, params object[] args) {
			if (Application.platform != RuntimePlatform.OSXEditor) return;
        
            string message = string.Format(format, args);
            AssertWithSkipFrames(3, condition, message);
        }

        public static void AssertIfNull(object testObject) {
            AssertWithSkipFrames(3, testObject != null, "null object!!!");
        }

        public static void AssertIfNullWithMessage(object testObject, string message) {
            AssertWithSkipFrames(3, testObject != null, message);
        }

        public static void AssertIfNullWithMessage(object testObject, string format, params object[] args) {
			if (Application.platform != RuntimePlatform.OSXEditor) return;
        
            string message = string.Format(format, args);
            AssertWithSkipFrames(3, testObject != null, message);
        }

        public static void AssertAlwaysFalse(string message) {
            AssertWithSkipFrames(3, false, message);
        }

		static void LogWithSkipFrames(int skipFrames, string message) {
			if (Application.platform != RuntimePlatform.OSXEditor) return;
		
            string fullMsg = GetTraceStackName(skipFrames) + message;
            UnityEngine.Debug.Log(fullMsg);
        }

        static void AssertWithSkipFrames(int skipFrames, bool condition, string message) {
			if (Application.platform != RuntimePlatform.OSXEditor) return;
        
            if (condition == true) {
                return;
            }

            string fullMessage = GetTraceStackName(skipFrames) + message;
            UnityEngine.Debug.LogError(fullMessage);
            UnityEngine.Debug.Break();

            throw new Exception();
        }
    }
}