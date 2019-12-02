﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        public static List<Movie> All
        {
            get
            {
                if(movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> Search(List<Movie> movies, string searchString)
        {
            if (searchString == null) return All;
            List<Movie> result = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(movie);
                }
            }
            return result;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
        {
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(mpaa.Contains(movie.MPAA_Rating))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        public static List<Movie> Filter(List<string> ratings)
        {
            List<Movie> result = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if(ratings.Contains(movie.MPAA_Rating))
                {
                    result.Add(movie);
                }
            }
            return result;
            
        }
        public static List<Movie> SearchAndFilter(string searchString, List<string> ratings)
        {
            List<Movie> result = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if (movie.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                    && ratings.Contains(movie.MPAA_Rating))
                {
                    result.Add(movie);
                }
            }
            return result;
        }

        public static List<Movie> FilterByMinIMDB(List<Movie> movies, float minIMDB)
        {
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if (movie.IMDB_Rating >= minIMDB)
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        public static List<Movie> Sort(List<Movie> movies)
        {
            return movies;
        }
    }
}
