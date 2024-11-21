import { useState } from "react";
import FilmModal from "./FilmModal";

const FilmContainer = () => {
    const [films, setFilms] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);

    const addFilm = (film) => {
        setFilms((prev) => [...prev, film]);
    };

    return (
        <div className="p-4 border rounded shadow-md w-full max-w-lg bg-white">
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-lg font-bold">Film Container</h2>
                <button
                    onClick={() => setIsModalOpen(true)}
                    className="bg-blue-600 text-white px-3 py-1 rounded shadow"
                >
                    Add Film
                </button>
            </div>
            <div className="flex gap-4 overflow-x-auto">
                {films.length > 0 ? (
                    films.map((film, index) => (
                        <div
                            key={index}
                            className="w-32 h-48 border rounded shadow-sm bg-gray-200 flex items-center justify-center"
                        >
                            <img
                                src={film.image}
                                alt={film.title}
                                className="object-cover w-full h-full rounded"
                            />
                        </div>
                    ))
                ) : (
                    <p className="text-gray-500">No films added yet.</p>
                )}
            </div>
            <FilmModal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onAddFilm={addFilm}
            />
        </div>
    );
};

export default FilmContainer;
