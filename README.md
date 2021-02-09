# Mefino [![Mefino](https://raw.githubusercontent.com/Mefino/Mefino/main/img/logo_32x32.png)]()

Mefino is a work-in-progress Mod Manager for Outward.

Mefino is currently in Alpha testing phase.

## How to use

<b>Please remove (or make a backup of) all existing mods before installing / testing Mefino.</b> This is not technically required, but the safety of your non-Mefino files is not guaranteed while using Mefino.

1. Download `Mefino.exe` from the release page
2. Run it :)

## Temporary Release Guide

1. Create a file called `manifest.json` using the example below.
2. Put the contents of your release inside a zip file, not in any sub-folders. Your manifest does <b>not</b> need to be included in your actual release file.
3. Name your zip file `mefino-package.zip`.
4. Create a repository (the name of this repo wil be the name for your release), and make a new release. Include your `mefino-package.zip` in the release.
5. Your `manifest.json` <b>must be in the root folder of your repository!</b> Make sure you update it for each release.
6. Make sure your repository has a `README.md` file, and anywhere in the README you must put the phrase `outward mefino mod` (case insensitive and special characters ignored). 

That's all there is to it, your package should now appear in the Mefino browser.

For an example release, you can see [here](https://github.com/Mefino/Mefino.Plugin) for now.

### Local install

To install a package locally for testing:
1. Create a folder in the `BepInEx\plugins\` folder which matches your GUID (`authorName repositoryName`)
2. Unzip your `mefino-package.zip` contents inside here
3. Put your `manifest.json` inside the folder too, and make sure you DO set the `author` and `name`

### Package GUIDs

Mefino package GUIDs are always `githubUsername repositoryName`, for example `sinai-dev Outward-SideLoader` or `Mefino Mefino.Plugin`.
* <b>note the space between the author name and the repository name!</b>

### Mefino Manifest

* Please use a JSON validator such as [this](https://jsonlint.com/) to ensure your JSON is valid before a release or update
* The file must be called `manifest.json`

```json
{
	"author": "MyGithubUsername",
	"name": "MyRepositoryName",
	"version": "1.0.0.0",
	"description": "A short description of one or two sentences.",
	"dependencies": [
		"someAuthor someRepository"
	],
	"conflicts_with": [
	],
	"require_sync": false,
	"override_folder": ""
}
```

* [optional] `author` and `name` are your GitHub username and Repository name, but they are not required for web releases. If you install a package locally (manually), you <b>do</b> need to set these.
* `version` is the version of your latest release, eg `1.0.0.0`
* `description` is a short description of your package, just one or two sentences.
* `dependencies` is a list of other package GUIDs which your package depends on.
* `conflicts_with` is also a list of GUIDs, but these are packages which your mod conflicts with (meaning they CANNOT be active at the same time).
* `require_sync` is whether your mod should be installed by all players online, it must be `true` or `false`. In the future this will be used for "automatic" online mod syncing.
* [optional] `override_folder` is an optional special name you can use for the install directory, instead of your repository name. This does not affect your GUID.