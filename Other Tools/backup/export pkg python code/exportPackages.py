#!/usr/bin/env python

import os
import platform
import datetime

osx = "Darwin".lower()
win = "Windows".lower()

projectName = "MZSpritesKit"

today = datetime.date.today()
versionStrginFromDate = str(today.year) + "." + str(
    today.month) + "." + str(today.day)

pkgsrcs = [
    "Assets/Plugins",
    "Assets/Plugins/MZSprites/Core"
]

pkgdsts = [
    "../unitypackage/MZSpritesKit" + "-v." + versionStrginFromDate + ".unitypackage",
    "../unitypackage/MZSprites" + "-v." + versionStrginFromDate + ".unitypackage"
]

executePath = ""


def getExecutePath():
    if platform.system().lower() == osx:
        return "/Applications/Unity/Unity.app/Contents/MacOS/Unity"

    if platform.system().lower() == win:
        return "C:\Program Files (x86)\Unity\Editor\Unity.exe"

    print "not support os type"
    exit(1)


def setUnityToTargetProjectWithExecutePath(executePath):
    parojectPath = os.getcwd() + "/MZSpritesKit"
    os.system(executePath + " -batchMode -quit -projectPath " + parojectPath)


def getCommandsWithExecutePath(executePath):
    commands = []
    for exportCommand in getExportCommandsWithExecutePath(executePath):
        commands.append(exportCommand)

    return commands


def getExportCommandsWithExecutePath(executePath):
    commands = []
    exportCommand = "-batchmode -quit -exportPackage"

    for i in range(len(pkgsrcs)):
        commands.append(
            executePath + " " + exportCommand + " " + pkgsrcs[i] + " " + pkgdsts[i])

    return commands


def executeCommands(commands):
    for command in commands:
        os.system(command)

if __name__ == "__main__":
    executePath = getExecutePath()
    setUnityToTargetProjectWithExecutePath(executePath)
    commands = getCommandsWithExecutePath(executePath)
    executeCommands(commands)
