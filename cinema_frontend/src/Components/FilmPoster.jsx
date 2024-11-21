import React from "react";

const FilmPoster = ({ imageUrl }) => {
    return (
        <div className="relative w-64 h-96 overflow-hidden group">
            {/* Background Image */}
            <div
                className="absolute inset-0 bg-cover bg-center transition-all duration-500 group-hover:blur-md group-hover:brightness-50"
                style={{backgroundImage: `url(${imageUrl})`}}
            ></div>

            {/* Overlay Content */}
            <div
                className="absolute inset-0 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity duration-500">
                <div className="text-white text-center">
                    {overlayContent}
                </div>
            </div>
        </div>
    );
};

export default FilmPoster;