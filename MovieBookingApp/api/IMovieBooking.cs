
namespace api
{
    public interface IMovieBooking
    {
        public int CreateUser(User user);
        public User GetUser(int userId);

        public int CreateMovie(Movie movie);
        public Movie GetMovie(int movieId);
        public List<Movie> GetMovies(string searchText);

        public int CreateTheater(Theater theater);
        public Theater GetTheater(int theaterId);
        public List<Movie> GetMoviesInTheater(int theaterId);

        public int CreateShow(Show show);
        public Show GetShow(int showId);

        public int CreateTicket(Ticket ticket);
        public Ticket GetTicket(int ticketId);
    }
}