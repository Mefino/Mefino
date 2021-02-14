<p align="center">
  <img align="center" src="https://raw.githubusercontent.com/Mefino/Mefino/main/img/banner_350x128.png">
</p>

<p align="center">
  Mefino is a work-in-progress Mod Manager for Outward.<br><br>

  <a href="https://discord.gg/zKyfGmy7TR">
    <img src="https://img.shields.io/discord/293810842225606656.svg?color=7289da&label=Discord&logo=discord&style=flat-square" />
  </a>
  
</p>

## How to use

0. Recommended to remove all existing mods first.
1. Download `Mefino.exe` from the release page
2. Run it :) 

## Temporary Release Guide

NOTES:
* Mefino will not refresh manifests in a repository if the repo has been updated in the last 5 minutes, due to how GitHub caches its `raw` CDN.
* For this reason, please wait 5 minutes after publishing your manifest for it to appear in Mefino.

Release process:
1. Create a new repository on GitHub for your package, or use an existing one if you have one for the mod already.
2. Create a file called `mefino-manifest.json` in the root folder of the repository using the example below, and push it to GitHub.
3. Put the contents of your release inside a zip file. <b>The name of the zip is important, it must match the `download_filename` of the package (or the `name` if you do not set a filename).</b>
4. Make a new release in your repository, and include your zip file in the release.
5. Create or edit the `README.md` file in your repository, and put the phrase `outward mefino mod` (case insensitive and special characters ignored) anywhere in the readme. See existing packages for possible examples, it doesn't really matter how you do it as long as your package now shows up in the Mefino browser.

For an example release, you can see [here](https://github.com/Mefino/Mefino.Plugin) for now.

### Package GUIDs

Mefino uses GUIDs (global unique IDs) to identify packages. Half of the GUID is determined by your GitHub username (or `author` for manually-installed packages), and the other half is determined by the `name` for your package.

The format of the GUID is `{author} {name}`. <i>Note the space between the author name and the repository name.</i> For example `sinai-dev SideLoader` or `Mefino Mefino.Plugin`. 

### Manifest

* Please use a JSON validator such as [this](https://jsonlint.com/) to ensure your JSON is valid before a release or update. Be careful with the commas on your dependency and conflict lists.
* The file must be called `mefino-manifest.json`

Empty JSON template:
```json
{
  "author": "",
  "name": "",
  "version": "",
  "required_version": "",
  "description": "",
  "tags": [
  ],
  "dependencies": [
  ],
  "conflicts_with": [
  ],
  "require_sync": false,
  "download_filename": ""
}
```

#### Required

`name` (string)
* The name for your mod or library. This will affect the install folder of your package, and will be the display name in the Mefino launcher.

`version` (string)
* The version of your latest release, eg `1.0.0.0`
* You must use Semantic versioning (`Major.Minor.Patch.Build`), though only `Major` is required.

`required_version` (string)
* Any versions more recent than this version will be <b>optional</b> updates.
* You can change this whenever you want, for example if you release a big update you can set this value to the same version, and all users will be required to update.
* Likewise, if you make a minor update and don't want to force all users to re-download your entire package, you can leave this on the previous value.
* Like `version`, this must be a semantic version string (eg `1.0.0`)
* If not set at all, it will default to the same as `version` (ie, every update will be required).

`description` (string)
* A short description for your package, just one or two sentences.

#### Optional

`author` (string)
* Your GitHub Username where this package is hosted. This is <b>not required</b> to be set for web releases, but if you install a local package manually you <b>do</b> need to set this for Mefino to work properly.
* <b>If you make a fork of a repository</b>, you <b>do</b> need to set the `author` to your own username. By default, forks are hidden from search results until you set that.

`tags` (list of strings)
* A list of tags for your package, used for filtering on the "Browse Mods" tab of Mefino.
* You can <b>only</b> use these accepted tags (case insensitive): `Balancing`, `Characters`, `Classes`, `Items`, `Library`, `Mechanics`, `Quests`, `Skills`, `Utility`, `UI`
* You can suggest another tag in the Discord if you want.
* The `Library` tag will mean your package is hidden unless the user enables "Show Libraries".

`dependencies` (list of strings)
* A list of other package GUIDs which your package depends on.

`conflicts_with`  (list of strings)
* A list of other package GUIDs which your mod conflicts with (meaning they CANNOT be active at the same time).

`require_sync` (boolean)
* Whether your mod should be installed by all players online, it must be `true` or `false`. In the future this will be used for "automatic" online mod syncing.

`download_filename` (string)
* You can use this to set an alternate filename for your package download, ie. Mefino will try to download `<download_filename>.zip`. You will need to upload a zip file with this name in your latest release for the repository. If you don't set this, Mefino will expect to find a `<name>.zip` file in your latest release instead. 

### Multi-package Repositories

You can host multiple packages in one repository by using a different type of manifest.

You can do this by defining a `packages` list in your JSON instead, your `mefino-manifest.json` should look like this:

Notes: 
* `//`-Comments are not valid JSON.
* You will need to set a unique `download_filename` for each package.
* You are allowed to set the `author` as a base entry in the JSON, you don't have to set it for each package individually.

```
{
  "author": "",
  "packages": [
    {
      "name": "MyPlugin1",
      // ... etc, fill out manifest as normal here
    },
    {
      "name": "MyOtherPlugin",
      // etc...
    } // remember to NOT put a comma after your last package.
  ]
}
```

### Local installs

To install a package locally for testing:
1. Create a folder in the `BepInEx\plugins\` folder which matches your GUID (`{author} {name}`).
2. Put your mefino package contents inside here
3. Put your `mefino-manifest.json` (must be a singular manifest, not a multi-package manifest) inside the folder too, and make sure you set the `author` and `name` correctly.
