import React, { useEffect, useState } from "react";
import FilmPoster from "./FilmPoster.jsx";

const FilmPosterList = () => {
    const [currentMovies, setCurrentMovies] = useState([]);

    const API_URL = "https://api.example.com/movies";

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await fetch(API_URL);
                const data = await response.json();
                setCurrentMovies(data.results);
            } catch (error) {
                console.error("Error fetching movies:", error);
            }
        };

        fetchMovies();
    }, []);

    return (
        <div className="relative w-full overflow-x-auto whitespace-nowrap scrollbar-hide">
            <div className="flex space-x-4 px-4">
                {currentMovies.map((movie) => (
                    <div key={movie.id} className="inline-block">
                        <FilmPoster
                            imageUrl={movie.poster_path}
                        />
                    </div>
                ))}
            </div>
        </div>
    );
};

export default FilmPosterList;