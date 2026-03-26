using Newtonsoft.Json;
using Ukebook.Models;

namespace Ukebook.Services;

public sealed class SongService
{
    private static readonly string AppDataPath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Ukebook");
    private static readonly string SongsPath = Path.Combine(AppDataPath, "Songs");
    private static readonly string IndexPath = Path.Combine(AppDataPath, "songs_index.json");

    public static void EnsureDataDirectory()
    {
        Directory.CreateDirectory(AppDataPath);
        Directory.CreateDirectory(SongsPath);
    }

    public List<Song> LoadAllSongs()
    {
        EnsureDataDirectory();
        if (!File.Exists(IndexPath))
        {
            var samples = CreateSampleSongs();
            PersistIndex(samples);
            return samples;
        }
        try
        {
            var songs = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(IndexPath)) ?? [];
            foreach (var song in songs)
            {
                var path = Path.Combine(SongsPath, $"{song.Id}.cho");
                if (File.Exists(path)) song.ChordProContent = File.ReadAllText(path);
            }
            return [.. songs.OrderBy(s => s.Artist).ThenBy(s => s.Title)];
        }
        catch { return []; }
    }

    public void SaveSong(Song song)
    {
        EnsureDataDirectory();
        song.DateModified = DateTime.Now;
        File.WriteAllText(Path.Combine(SongsPath, $"{song.Id}.cho"), song.ChordProContent);
        var songs = LoadAllSongs();
        int idx = songs.FindIndex(s => s.Id == song.Id);
        if (idx >= 0) songs[idx] = song; else songs.Add(song);
        PersistIndex(songs);
    }

    public void DeleteSong(Song song)
    {
        var path = Path.Combine(SongsPath, $"{song.Id}.cho");
        if (File.Exists(path)) File.Delete(path);
        var songs = LoadAllSongs();
        songs.RemoveAll(s => s.Id == song.Id);
        PersistIndex(songs);
    }

    private static void PersistIndex(List<Song> songs)
    {
        var index = songs.Select(s => new Song
        {
            Id = s.Id, Title = s.Title, Artist = s.Artist, Genre = s.Genre,
            Key = s.Key, Capo = s.Capo, Tempo = s.Tempo, FilePath = s.FilePath,
            DateAdded = s.DateAdded, DateModified = s.DateModified,
            ChordProContent = string.Empty
        }).ToList();
        File.WriteAllText(IndexPath, JsonConvert.SerializeObject(index, Formatting.Indented));
    }

    private static List<Song> CreateSampleSongs() =>
    [
        new Song
        {
            Title = "Somewhere Over the Rainbow", Artist = "Israel Kamakawiwoʻole",
            Genre = "Hawaiʻi", Key = "C",
            ChordProContent = """
                {title: Somewhere Over the Rainbow}
                {artist: Israel Kamakawiwoʻole}
                {key: C}
                {tempo: 70}

                {start_of_verse: Sloka 1}
                [C]Somewhere [Em]over the [F]rainbow, [C]way up [F]high
                [C]There's a [Em]land that I [F]heard of [Am]once in a [G]lullaby
                {end_of_verse}

                {start_of_chorus}
                [F]Someday I'll [C]wish upon a [Am]star
                And [F]wake up where the [C]clouds are far be[G]hind me
                {end_of_chorus}

                {chorus}
                """
        },
        new Song
        {
            Title = "Riptide", Artist = "Vance Joy", Genre = "Pop/Indie", Key = "Am",
            ChordProContent = """
                {title: Riptide}
                {artist: Vance Joy}
                {key: Am}
                {tempo: 96}

                {comment: Celá píseň: Am - G - C}

                {start_of_verse: Sloka 1}
                [Am]I was scared of [G]dentists and the [C]dark
                [Am]I was scared of [G]pretty girls and [C]starting conversations
                {end_of_verse}

                {start_of_chorus}
                Oh, and they [Am]come unstuck
                [G]Lady, running [C]down to the riptide
                [Am]Taken away to the [G]dark side
                {end_of_chorus}

                {chorus}
                """
        },
        new Song
        {
            Title = "I'm Yours", Artist = "Jason Mraz", Genre = "Pop", Key = "B", Capo = 2,
            ChordProContent = """
                {title: I'm Yours}
                {artist: Jason Mraz}
                {key: B}
                {tempo: 75}

                {comment: Kapo 2 – hrát akordy A - E - F#m - D}

                {start_of_verse: Sloka 1}
                [A]Well you done done me and you [E]bet I felt it
                I [F#m]tried to be chill but you're so hot that I [D]melted
                {end_of_verse}

                {start_of_chorus}
                But [A]I won't hesitate
                No [E]more, no more
                It [F#m]cannot wait, I'm [D]yours
                {end_of_chorus}

                {chorus}
                """
        }
    ];
}
