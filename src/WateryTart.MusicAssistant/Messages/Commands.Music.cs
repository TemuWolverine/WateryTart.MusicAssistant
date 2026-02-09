namespace WateryTart.MusicAssistant.Messages;

public static partial class Commands
{
    // Provider mapping
    public static string MusicAddProviderMapping = "music/provider/add_mapping";
    public static string MusicAlbumGet = "music/albums/get";
    public static string MusicAlbumLibraryItems = "music/albums/library_items";
    public static string MusicAlbumTracks = "music/albums/album_tracks";
    public static string MusicAlbumVersions = "music/albums/album_versions";

    // Artist
    public static string MusicArtistAlbums = "music/artists/artist_albums";
    public static string MusicArtistGet = "music/artists/get";
    public static string MusicArtistsCount = "music/artists/count";
    public static string MusicArtistsGet = "music/artists/library_items";
    public static string MusicArtistTracks = "music/artists/artist_tracks";

    // Audiobooks
    public static string MusicAudiobooksAudiobookVersions = "music/audiobooks/audiobook_versions";
    public static string MusicAudiobooksCount = "music/audiobooks/count";
    public static string MusicAudiobooksGet = "music/audiobooks/get";
    public static string MusicAudiobooksLibraryItems = "music/audiobooks/library_items";

    // Browse & Favourites
    public static string MusicBrowse = "music/browse";
    public static string MusicFavouritesAddItem = "music/favourites/add_item";
    public static string MusicFavouritesRemoveItem = "music/favourites/remove_item";

    // Genres
    public static string MusicGenresCount = "music/genres/count";
    public static string MusicGenresGet = "music/genres/get";
    public static string MusicGenresLibraryItems = "music/genres/library_items";

    // Library Items & Item Management
    public static string MusicGetLibraryItem = "music/library/get_item";
    public static string MusicInProgressItems = "music/in_progress/items";
    public static string MusicItem = "music/item";
    public static string MusicItemByUri = "music/item/by_uri";
    public static string MusicLibraryAddItem = "music/library/add_item";
    public static string MusicLibraryRemoveItem = "music/library/remove_item";
    public static string MusicMarkPlayed = "music/item/mark_played";
    public static string MusicMarkUnplayed = "music/item/mark_unplayed";
    public static string MusicMatchProviders = "music/provider/match_providers";

    // Playlists
    public static string MusicPlaylistsAddPlaylistTracks = "music/playlists/add_playlist_tracks";
    public static string MusicPlaylistsCount = "music/playlists/count";
    public static string MusicPlaylistsCreatePlaylist = "music/playlists/create_playlist";
    public static string MusicPlaylistsLibraryItems = "music/playlists/library_items";
    public static string MusicPlaylistsRemovePlaylistTracks = "music/playlists/remove_playlist_tracks";
    public static string MusicPlaylistsGet = "music/playlists/get";
    public static string MusicPlaylistsPlaylistTracks = "music/playlists/playlist_tracks";

    // Podcasts
    public static string MusicPodcastsCount = "music/podcasts/count";
    public static string MusicPodcastsGet = "music/podcasts/get";
    public static string MusicPodcastsLibraryItems = "music/podcasts/library_items";
    public static string MusicPodcastsPodcastEpisode = "music/podcasts/podcast_episode";
    public static string MusicPodcastsPodcastEpisodes = "music/podcasts/podcast_episodes";
    public static string MusicPodcastsPodcastVersions = "music/podcasts/podcast_versions";

    // Radios
    public static string MusicRadiosCount = "music/radios/count";
    public static string MusicRadiosGet = "music/radios/get";
    public static string MusicRadiosLibraryItems = "music/radios/library_items";
    public static string MusicRadiosRadioVersions = "music/radios/radio_versions";

    // Recently played/added
    public static string MusicRecentlyAddedTracks = "music/tracks/recently_added";
    public static string MusicRecentlyPlayedTracks = "music/tracks/recently_played";

    // Recommendations & Refresh
    public static string MusicRecommendations = "music/recommendations";
    public static string MusicRefreshItem = "music/item/refresh";
    public static string MusicRemoveProviderMapping = "music/provider/remove_mapping";

    // Search & Sync
    public static string MusicSearch = "music/search";
    public static string MusicSync = "music/sync";
    public static string MusicSyncTasks = "music/sync/tasks";
    public static string MusicTrackByName = "music/tracks/by_name";
    public static string MusicTracksCount = "music/tracks/count";
    public static string MusicTracksTrackAlbum = "music/tracks/track_album";
    public static string MusicTracksTrackVersions = "music/tracks/track_versions";

    // Similar tracks
    public static string MusicSimilarTracks = "music/tracks/similar_tracks";
}