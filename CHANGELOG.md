# Changelog

## [4.2.0](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v4.1.0...v4.2.0) (2026-03-04)


### Features

* "order_by" enum, will eventually be rolled out to all methods that use ordering. ([0331a99](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/0331a99ddf48f272621e466adeda22005aded84d))
* order_by and search on GetPlaylistsAsync, GetMusicAlbumsLibraryItemsAsync, GetTracksAsync ([b68fe57](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/b68fe57b7da7526d1804a415ba69ea282d9596a6))


### Bug Fixes

* #nullable wrapping in code generation ([16a26f3](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/16a26f33476fd1ca3ce4651a2ac18c87cd3405ad))
* favourite-only now toggleable ([49a6af9](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/49a6af9f07fd75736721bde034c40da4a8b4ee11))
* OrderBy enum now gains descriptions ([29a94de](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/29a94de2fc5385dadaed14ba873234a902490f55))
* used vibe coding to copy across xmldoc into generated methods. Added some xmldoc ([eba9c53](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/eba9c53498627825187035b841dc8cfa2c944ac7))
* Ws and Rpc Artist methods now in sync ([7241beb](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/7241bebdf9ab4775b772fa45723c97bb478e5be6))

## [4.1.0](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v4.0.1...v4.1.0) (2026-02-26)


### Features

* Fleshed out GetArtistsAsync ([2535026](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/25350262a4bad0ac460c5a714bf66ef18a8fc6a7))


### Bug Fixes

* corrected Recently Added/Played track urls ([451d3be](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/451d3bef7560b7036d0c312346e91a8bd0e9aca6))
* Corrected recently played/added commands ([f36565a](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/f36565a3c04af030f61a59f107b773e986501954))

## [4.0.1](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v4.0.0...v4.0.1) (2026-02-25)


### Bug Fixes

* Added more [NotifyingProperty]'s to properties likely to be changed during a stream ([c1da4f9](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/c1da4f938978274fe9be0ee0accb445cbd47ddbe))

## [4.0.0](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v3.1.0...v4.0.0) (2026-02-24)


### ⚠ BREAKING CHANGES

* PlayerQueue.RepeatMode is now (correctly) an enum rather than just string.

### Features

* added more INPC for values that get pushed via events ([640cda7](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/640cda7c27aa7bfc7f37690e5ca702cd3030b1c3))


### Bug Fixes

* PlayerQueue.RepeatMode is now (correctly) an enum rather than just string. ([0aecd3d](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/0aecd3d4c35399bb42f2a84fa9bca1b232b11f48))

## [3.1.0](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v3.0.1...v3.1.0) (2026-02-19)


### Features

* added ClearPlayerQueueAsync which... clears the queue. ([3c15edf](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/3c15edfce3638222ac11c7405bc40d074a7fe051))
* added SetPlayerQueueDontStopTheMusicAsync ([9057256](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/9057256462c305869d720e965cac1dd6fe859b9a))
* Added SetPlayerQueueShuffleAsync. Used vibecoding to add a generator. On a Ws extension method, add [ToRpc] and it should generate the matching Rpc extension method ([1c9d6ad](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/1c9d6ada979ee912bef2333f33f189feb231b76d))


### Bug Fixes

* corrected shuffle_enabled ([80022a9](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/80022a947728c38b5801ee3f79111fdf328ac1b2))

## [3.0.1](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v3.0.0...v3.0.1) (2026-02-16)


### Features

* **Providers:** Added client.&lt;withApi&gt;.GetProviderManifestsAsync() ([f15b559](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/f15b559587b0b8e3be6fce2735958efe79e5642e))
* **Providers:** Added SvgHelper to extract Svg visual data into a xaml appropriate output given many provider mappings have svgIcon ([3668abc](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/3668abc52f4237797755cbd5e0c14575d2f5da9f))
* **Providers:** Added SvgHelper to extract Svg visual data into a xaml appropriate output given many provider mappings have svgIcon ([8b5215d](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/8b5215d843b7169565a8855ed3f5bc14dc1205e9))

## [3.0.0](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.9...v3.0.0) (2026-02-15)


### ⚠ BREAKING CHANGES

* Broken nugets should be solved now

### Bug Fixes

* Broken nugets should be solved now ([4c8332b](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/4c8332b82a6af06b54cad932ea33311c4209622c))

## [2.2.9](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.8...v2.2.9) (2026-02-15)


### Features

* Added SetPlayerQueueRepeatAsync(string QueueId, RepeatMode mode). Options are Off, One (single track), All (entire queue) ([c5f87a0](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/c5f87a0454222ce5cc6038a783945770f86d2ea6))


### Bug Fixes

* Cleaned up some compiler warnings ([fea89a1](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/fea89a150cbb5b8440e7246e2dc17814dc39a20c))

## [2.2.8](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.7...v2.2.8) (2026-02-14)


### Bug Fixes

* Changed readme, trying nuget release again ([4b6e5b2](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/4b6e5b2cde775ab9d888ffe92108079f0558deb8))

## [2.2.7](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.6...v2.2.7) (2026-02-14)


### Bug Fixes

* Missed permissions on autobuild & correctly ammended nuget push version ([7c32e2f](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/7c32e2fe331270c28fb615f7f604fbdccd2863f4))

## [2.2.6](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.5...v2.2.6) (2026-02-14)


### Bug Fixes

* Debug+new variable set ([7435214](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/743521446df3c8d37c9b55e6f88f7eb5a0a87348))
* removed gaslit by copilot code again ([c5a5fe6](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/c5a5fe6e2c0513d62eaf7f03cf632aeb05ed3842))

## [2.2.5](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.4...v2.2.5) (2026-02-14)


### Bug Fixes

* fixed versioning info in build file for nuget ([89ce1d0](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/89ce1d0268dd88bc8d38b99cda775fed06bc4300))

## [2.2.4](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.3...v2.2.4) (2026-02-14)


### Bug Fixes

* Release-Please ID now set ([5bf0d8e](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/5bf0d8e11c285f8dad7706bd39f682319f4aba34))

## [2.2.3](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.2...v2.2.3) (2026-02-14)


### Features

* Added lyric fetching, `var lyrics = await client.WithRpc().GetLyricsAsync(<track object>);` would return lyrics as an array of strings ([b09c05d](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/b09c05d9c3c804a541d9b114b7d4063d3cdc975a))
* GetLibraryItemAsync(type, id, source) added to both .WithRpc() and .WithWs() ([551fa42](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/551fa4220370252a3aa2d1a71d3edb0a1d7e857e))


### Bug Fixes

* Corrected how release-please auto-builds with if statements ([ccd933f](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/ccd933f848d79bff524ff9852cd64aa2565d7e2f))
* Corrected name for internal class (MediaAssistantJsonContext -&gt; MusicAssistantJsonContext) ([b09c05d](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/b09c05d9c3c804a541d9b114b7d4063d3cdc975a))
* Corrected name for internal class (MediaAssistantJsonContext -&gt; MusicAssistantJsonContext) ([288fa4a](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/288fa4a657365bec10cfb339b77ef042c724fc56))
* fixed MusicGetLibraryItem url ([551fa42](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/551fa4220370252a3aa2d1a71d3edb0a1d7e857e))
* Nuspec file being ignored, moved correct data to csproj ([984f583](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/984f583f0edb66836ade229dc4bb201dacd1bb07))
* Release-Please to auto-deploy to nuget ([3c0c98d](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/3c0c98d1759ebcd5c684b0042792da72e761bd72))
* Switched to WateryTart.MusicAssistant.Generators for INPC, due to the way CommunityMvvmToolkit exploded several times. ([551fa42](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/551fa4220370252a3aa2d1a71d3edb0a1d7e857e))

## [2.2.2](https://github.com/TemuWolverine/WateryTart.MusicAssistant/compare/v2.2.1...v2.2.2) (2026-02-13)


### Bug Fixes

* Found and suppressed more MVVMTK0033 warnings ([920ccdd](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/920ccdd92f0f4b49959634a623980182bf6c9f7c))
* Suppressed MVVMTK0033 warning ([68e2cf6](https://github.com/TemuWolverine/WateryTart.MusicAssistant/commit/68e2cf63acf8c80d897da9e236c815295623f142))
