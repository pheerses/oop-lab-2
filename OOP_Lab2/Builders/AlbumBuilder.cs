using OOP_Lab2.Models;

namespace OOP_Lab2.Builders
{
	public class AlbumBuilder
	{
		private Album _album;

		public AlbumBuilder StartNew(string title)
		{
			_album = new Album { Title = title };
			return this;
		}

		public AlbumBuilder SetArtist(Artist artist)
		{
			_album.Artist = artist;
			return this;
		}

		public AlbumBuilder SetGenre(Genre genre)
		{
			_album.Genre = genre;
			return this;
		}

		public AlbumBuilder SetReleaseYear(int year)
		{
			_album.ReleaseYear = year;
			return this;
		}

		public AlbumBuilder AddTrack(Track track)
		{
			_album.Tracks.Add(track);
			return this;
		}

		public Album Build()
		{
			return _album;
		}
	}

}
