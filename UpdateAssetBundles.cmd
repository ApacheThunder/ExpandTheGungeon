@echo off
copy ExpandAssets\Assets\AssetBundles\Windows\expandsharedauto ExpandTheGungeon\bin\Release\expandsharedauto /y
copy ExpandAssets\Assets\AssetBundles\Windows\expandaudio ExpandTheGungeon\bin\Release\expandaudio /y
copy ExpandShaders\Assets\AssetBundles\Windows\expandshaders ExpandTheGungeon\bin\Release\expandshaders /y
copy ExpandShaders\Assets\AssetBundles\Linux\expandshaders ExpandTheGungeon\bin\Release\expandshaders_linux /y
copy ExpandShaders\Assets\AssetBundles\MacOS\expandshaders ExpandTheGungeon\bin\Release\expandshaders_macos /y
pause
