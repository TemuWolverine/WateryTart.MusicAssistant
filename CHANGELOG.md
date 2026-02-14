# Changelog

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
