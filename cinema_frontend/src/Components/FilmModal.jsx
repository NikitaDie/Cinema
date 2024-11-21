import { useState } from "react";

const FilmModal = ({ isOpen, onClose, onAddFilm }) => {
    const [selectedFilm, setSelectedFilm] = useState(null);

    const films = [
        { title: "Film 1", image: "/images/film1.jpg" },
        { title: "Film 2", image: "/images/film2.jpg" },
        { title: "Film 3", image: "/images/film3.jpg" },
    ];

    const handleAdd = () => {
        if (selectedFilm) {
            onAddFilm(selectedFilm);
            onClose();
        }
    };

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
            <div className="bg-white p-6 rounded shadow-lg w-96">
                <h2 className="text-lg font-bold mb-4">Select a Film</h2>
                <ul className="space-y-2">
                    {films.map((film, index) => (
                        <li
                            key={index}
                            className={`p-2 border rounded cursor-pointer ${
                                selectedFilm?.title === film.title
                                    ? "bg-blue-100"
                                    : ""
                            }`}
                            onClick={() => setSelectedFilm(film)}
                        >
                            {film.title}
                        </li>
                    ))}
                </ul>
                <div className="mt-4 flex justify-end gap-2">
                    <button
                        onClick={onClose}
                        className="bg-gray-400 text-white px-3 py-1 rounded"
                    >
                        Cancel
                    </button>
                    <button
                        onClick={handleAdd}
                        className="bg-blue-600 text-white px-3 py-1 rounded"
                    >
                        Add Film
                    </button>
                </div>
            </div>
        </div>
    );
};

export default FilmModal;