namespace WateryTart.MusicAssistant.Messages;

public static partial class Commands
{
    // Provider mapping
    public const string MusicAddProviderMapping = "music/provider/add_mapping";
    public const string MusicAlbumGet = "music/albums/get";
    public const string MusicAlbumsCount = "music/albums/count";
    public const string MusicAlbumLibraryItems = "music/albums/library_items";
    public const string MusicAlbumTracks = "music/albums/album_tracks";
    public const string MusicAlbumVersions = "music/albums/album_versions";

    // Artist
    public const string MusicArtistAlbums = "music/artists/artist_albums";
    public const string MusicArtistGet = "music/artists/get";
    public const string MusicArtistsCount = "music/artists/count";
    public const string MusicArtistsGet = "music/artists/library_items";
    public const string MusicArtistTracks = "music/artists/artist_tracks";

    // Audiobooks
    public const string MusicAudiobooksAudiobookVersions = "music/audiobooks/audiobook_versions";
    public const string MusicAudiobooksCount = "music/audiobooks/count";
    public const string MusicAudiobooksGet = "music/audiobooks/get";
    public const string MusicAudiobooksLibraryItems = "music/audiobooks/library_items";

    // Browse & Favourites
    public const string MusicBrowse = "music/browse";
    public const string MusicFavouritesAddItem = "music/favorites/add_item";
    public const string MusicFavouritesRemoveItem = "music/favorites/remove_item";

    // Genres
    public const string MusicGenresCount = "music/genres/count";
    public const string MusicGenresGet = "music/genres/get";
    public const string MusicGenresLibraryItems = "music/genres/library_items";

    // Library Items & Item Management
    public const string MusicGetLibraryItem = "music/library/get_item";
    public const string MusicInProgressItems = "music/in_progress/items";
    public const string MusicItem = "music/item";
    public const string MusicItemByUri = "music/item/by_uri";
    public const string MusicLibraryAddItem = "music/library/add_item";
    public const string MusicLibraryRemoveItem = "music/library/remove_item";
    public const string MusicMarkPlayed = "music/item/mark_played";
    public const string MusicMarkUnplayed = "music/item/mark_unplayed";
    public const string MusicMatchProviders = "music/provider/match_providers";

    // Playlists
    public const string MusicPlaylistsAddPlaylistTracks = "music/playlists/add_playlist_tracks";
    public const string MusicPlaylistsCount = "music/playlists/count";
    public const string MusicPlaylistsCreatePlaylist = "music/playlists/create_playlist";
    public const string MusicPlaylistsLibraryItems = "music/playlists/library_items";
    public const string MusicPlaylistsRemovePlaylistTracks = "music/playlists/remove_playlist_tracks";
    public const string MusicPlaylistsGet = "music/playlists/get";
    public const string MusicPlaylistsPlaylistTracks = "music/playlists/playlist_tracks";

    // Podcasts
    public const string MusicPodcastsCount = "music/podcasts/count";
    public const string MusicPodcastsGet = "music/podcasts/get";
    public const string MusicPodcastsLibraryItems = "music/podcasts/library_items";
    public const string MusicPodcastsPodcastEpisode = "music/podcasts/podcast_episode";
    public const string MusicPodcastsPodcastEpisodes = "music/podcasts/podcast_episodes";
    public const string MusicPodcastsPodcastVersions = "music/podcasts/podcast_versions";

    // Radios
    public const string MusicRadiosCount = "music/radios/count";
    public const string MusicRadiosGet = "music/radios/get";
    public const string MusicRadiosLibraryItems = "music/radios/library_items";
    public const string MusicRadiosRadioVersions = "music/radios/radio_versions";

    // Recently played/added
    public const string MusicRecentlyAddedTracks = "music/tracks/recently_added";
    public const string MusicRecentlyPlayedTracks = "music/tracks/recently_played";
    public const string MusicTracksLibraryItems = "music/tracks/library_items";

    // Recommendations & Refresh
    public const string MusicRecommendations = "music/recommendations";
    public const string MusicRefreshItem = "music/item/refresh";
    public const string MusicRemoveProviderMapping = "music/provider/remove_mapping";

    // Search & Sync
    public const string MusicSearch = "music/search";
    public const string MusicSync = "music/sync";
    public const string MusicSyncTasks = "music/sync/tasks";
    public const string MusicTrackByName = "music/tracks/by_name";
    public const string MusicTracksCount = "music/tracks/count";
    public const string MusicTracksTrackAlbum = "music/tracks/track_album";
    public const string MusicTracksTrackVersions = "music/tracks/track_versions";

    // Similar tracks
    public const string MusicSimilarTracks = "music/tracks/similar_tracks";
}