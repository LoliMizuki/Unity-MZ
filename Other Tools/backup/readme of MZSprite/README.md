Unity-MZSprites
===============

2D sprites plugin, core and editor. base on Unity 4.3+, 2D features.

# Features

- Create spritesheets by 3rd tools, make your asserts partable in any projects. no more restricted by Unity.
- Create sprite frame animations by editor or **CODE**. animation info is Json readable format, also **PORTABLE** to any where.
- Auto Generate animations from spritesheet.

# Support Spritesheet Tools

- Shoebox: [http://renderhjs.net/shoebox/](http://renderhjs.net/shoebox/)
- Texture Packer: [http://www.codeandweb.com/texturepacker](http://www.codeandweb.com/texturepacker) **Not yet**

# Manual
Sorry ... Coming Soon ...

# Repo

- MZSpritesKit/: Unity project.
- RawAssets/: Resources for test.
- src/: Source code, **`but not synchronous with Unity project not ... so do't use`**.
- unitypackage/: Plugin unitypackage file.
    + MZSprites: Core only.
    + MZSpritesKit: Core + Editor tool.
- exportPackage.py: Export plugin package to unitypackage/.
    - Limitations of exportPackage.py
        + It call unity command tool so always start unity.app, and unity allow only one instance at same time. all you can do is close current unity and execute this py :D
        + It also need unity set to build target project.
        + Support run in Windows OS, but not tested :D
    - Issues
        + Unity command -exportPackage only do once in one batchMode? I can't export one more package at same time.

# Known Issues

- Sprite objects leak on saving project(Unity will clear them).

# Future Works

- Support Unity Physics2D.
- Animation preview in AnimationSetting Window.
- Sprite Action(as cocos2d's CCAction, apple SpriteKit's SKAction).
- `UI Editor` base on this core.
- Support Unity built-in animation.
- More tool support(ex: [TexturePacker](http://www.codeandweb.com/texturepacker)).
- add test code.

