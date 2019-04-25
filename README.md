# ![windows][windows] PathEd

Without PathEd, it can be quite cumbersome to handle the old-school semicolon-devided PATH variable safely. 

PathEd has no dependencies besides the .NET Framework 2.0 and can be deployed with any installer, for example.
Editing the PATH within your setup is just one single exec command instead of using proprietary PATH manipulation plugins, etc.

## Examples
###### Add a value to the Windows PATH:
`PathEd.exe add "C:\Program Files\RepoZ"`

###### Remove a value from the Windows PATH:
`PathEd.exe remove "C:\Program Files\RepoZ"`

###### Usage in a NSIS install script:
PathEd is used in the [RepoZ](https://github.com/awaescher/RepoZ) NSIS install script to [add](https://github.com/awaescher/RepoZ/blob/496d4f7539670112772b81e208c2ce650164e101/_setup/RepoZ.nsi#L59) and [remove](https://github.com/awaescher/RepoZ/blob/496d4f7539670112772b81e208c2ce650164e101/_setup/RepoZ.nsi#L92) the application location to the Windows PATH.


## Noteworthy

PathEd will ...
- ... just add values if they are new to the PATH. So it can be called multiple times.
- ... ignore removals if the value is not part of the PATH. So it can be called multiple times as well.
- ... compare values against the PATH (whether it should add or can remove values) case-insensitive.
- ... remove values even if the PATH defines them in quotes.

As always, you'll need to add quotes to the value if it contains spaces (like shown in the examples). Otherwise, Windows will split them up as multiple arguments.

[windows]: https://raw.githubusercontent.com/MarcBruins/awesome-xamarin/master/images/windows.png
