#define DEBUG

using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;

static public partial class MZ {

    public class Debugs {
    
        static public string GetTraceStackName(int skipFrames) {
            StackTrace stackTrace = new System.Diagnostics.StackTrace(skipFrames);
            int frameIndex = 0;
            StackFrame stackFrame = stackTrace.GetFrame(frameIndex);

            return stackFrame.GetMethod().DeclaringType.FullName + "." + stackFrame.GetMethod().Name + "(): ";
        }

        static public void Trace(int skipFrames) {
            UnityEngine.Debug.Log(GetTraceStackName(skipFrames));
        }

        static public void Trace() {
            // caller -> Trace() --> Trace(skipframes)
            Trace(3);
        }

        static public void Log(string message) {
            if (Application.platform != RuntimePlatform.OSXEditor) {
                return;
            }

            LogWithSkipFrames(3, message);
        }

        static public void Log(string format, params object[] args) {
            string message = string.Format(format, args);
            LogWithSkipFrames(3, message);
        }

        static public void LogBreak(string message) {
            LogWithSkipFrames(3, message);
            UnityEngine.Debug.Break();
        }

        static public bool Alert(bool condition, string message) {
            if (condition == true) {
                string fullMsg = GetTraceStackName(2) + message;

                UnityEngine.Debug.Log(fullMsg);
            }

            return condition;
        }

        static public void Assert(bool condition, string message) {
            AssertWithSkipFrames(3, condition, message);
        }

        static public void Assert(bool condition, string format, params object[] args) {
            string message = string.Format(format, args);
            AssertWithSkipFrames(3, condition, message);
        }

        static public void AssertIfNull(object testObject) {
            AssertWithSkipFrames(3, testObject != null, "null object!!!");
        }

        static public void AssertIfNullWithMessage(object testObject, string message) {
            AssertWithSkipFrames(3, testObject != null, message);
        }

        static public void AssertIfNullWithMessage(object testObject, string format, params object[] args) {
            string message = string.Format(format, args);
            AssertWithSkipFrames(3, testObject != null, message);
        }

        static public void AssertAlwaysFalse(string message) {
            AssertWithSkipFrames(3, false, message);
        }

    #region - Private

        static void LogWithSkipFrames(int skipFrames, string message) {
            string fullMsg = GetTraceStackName(skipFrames) + message;
            UnityEngine.Debug.Log(fullMsg);
        }

        static void AssertWithSkipFrames(int skipFrames, bool condition, string message) {
            if (condition == true) {
                return;
            }

            string fullMessage = GetTraceStackName(skipFrames) + message;
            UnityEngine.Debug.LogError(fullMessage);
            UnityEngine.Debug.Break();

            throw new Exception();
        }

    #endregion
    }
}