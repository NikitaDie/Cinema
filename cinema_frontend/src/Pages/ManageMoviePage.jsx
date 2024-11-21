import { useState } from "react";
import UploadFilesZone from "../Components/UploadFileZone";

const formTextFields = [
    { label: "Movie Name", name: "title", type: "text" },
    { label: "Release Year", name: "releaseYear", type: "number" },
    { label: "Original Title", name: "originalTitle", type: "text" },
    { label: "Director", name: "director", type: "text" },
    { label: "Language", name: "language", type: "text" },
    { label: "Genre", name: "genre", type: "text" },
    { label: "Duration (e.g., 2h 30m)", name: "duration", type: "text" },
    { label: "Producer", name: "producer", type: "text" },
    { label: "Production Studio", name: "productionStudio", type: "text" },
    { label: "Screenplay", name: "screenplay", type: "text" },
    { label: "Starring", name: "starring", type: "text" },
    { label: "Inclusive Adaptation", name: "inclusiveAdaptation", type: "text" },
    { label: "Description", name: "description", type: "textarea" },
    { label: "Trailer Link", name: "trailerLink", type: "url" },
];

const ageRatingMap = {
    "G": 0,
    "PG": 6,
    "PG-13": 13,
    "R": 17,
    "NC-17": 18,
};

const ManageMoviePage = () => {
    const [data, setData] = useState({
        title: "", // String, required
        ageRating: "", // Integer, required (can start as an empty string and validated later)
        releaseYear: "", // Integer, required
        originalTitle: "", // String, optional
        director: "", // String, required
        rentalPeriodStart: null, // String (date-time), required
        rentalPeriodEnd: null, // String (date-time), required
        language: "", // String, required
        genres: [], // Array, required
        duration: "", // String (date-span), required
        producer: "", // String, optional
        productionStudio: "", // String, optional
        screenplay: "", // String, optional
        starring: [], // Array, optional
        inclusiveAdaptation: "", // String, optional
        description: "", // String, optional
        trailerLink: "", // String (URI), required
        image: null, // File object (binary), required
    });

    const [errors, setErrors] = useState({});

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!validate()) {
            alert("Please fix validation errors.");
            return;
        }

        const formData = new FormData();
        for (let field in data) {
            if (data[field]) {
                formData.set(field, data[field]);
            }
        }

        formData.set("duration", parseDuration(data.duration));
        formData.set("ageRating", ageRatingMap[data.ageRating])

        if (data.genres) formData.set("genres", JSON.stringify(data.genres));
        if (data.starring) formData.set("starring", JSON.stringify(data.starring));

        for (let [key, value] of formData.entries()) {
            console.log(`${key}:`, value);
        }

        try {
            const response = await fetch("/api/movies", {
                method: "POST",
                body: formData,
            });
            console.log("Hello");
            if (!response.ok) {
                const result = await response.json();
                const formattedErrors = formatErrors(result.errors);
                alert(`Error ${response.status}:\n\n${formattedErrors}`);
                return;
            }

            alert("Movie added successfully!");
        } catch (error) {
            console.error("Error submitting form:", error.message);
            alert("There was an error while adding the movie.");
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setData((prev) => ({ ...prev, [name]: value }));
    };

    const handleFileChange = (fileData) => {
        //todo remove null?
        if (fileData && fileData.file) {
            const file = fileData.file;
            const objectUrl = URL.createObjectURL(file);
            setData((prev) => ({ ...prev, image: objectUrl }));
        }
    };

    const validate = () => {
        const newErrors = {};

        const requiredFields = [
            "title", "ageRating", "releaseYear", "director", "language", "trailerLink", "rentalPeriodStart", "rentalPeriodEnd",
        ];

        requiredFields.forEach((field) => {
            if (!data[field]) newErrors[field] = "This field is required.";
        });

        if (data.ageRating && !ageRatingMap[data.ageRating]) {
            newErrors.ageRating = "Select a valid age restriction.";
        }

        if (data.releaseYear && (isNaN(data.releaseYear) || data.releaseYear < 1800 || data.releaseYear > new Date().getFullYear())) {
            newErrors.releaseYear = "Enter a valid year between 1800 and the current year.";
        }

        if (data.trailerLink && !/^https?:\/\/\S+$/.test(data.trailerLink)) {
            newErrors.trailerLink = "Enter a valid URL (must start with http or https).";
        }

        if (data.rentalPeriodStart && data.rentalPeriodEnd && new Date(data.rentalPeriodStart) >= new Date(data.rentalPeriodEnd)) {
            newErrors.rentalPeriodEnd = "End date must be after start date.";
        }

        if (data.duration && !parseDuration(data.duration)) {
                newErrors.duration = "Enter a valid duration in the format 'xh ym' (e.g., '2h 30m').";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const parseDuration = (duration) => {
        const regex = /^(\d+h)?\s*(\d+m)?$/;  // Regex to match hours and minutes
        const match = duration.match(regex);

        if (!match) return null; // Invalid format

        let hours = 0;
        let minutes = 0;

        if (match[1]) {
            hours = parseInt(match[1].replace("h", ""), 10);
        }
        if (match[2]) {
            minutes = parseInt(match[2].replace("m", ""), 10);
        }

        // Ensure the parsed values are valid
        if (hours >= 0 && minutes >= 0) {
            return `${hours}:${minutes}`;  // Return the parsed duration object
        }

        return null;  // Return null for invalid duration
    };

    const formatErrors = (errors) => {
        return Object.entries(errors).map(([field, messages]) => `${field}: ${messages.join(", ")}`).join("\n");
    };

    const getInputClassName = (fieldName) => {
        return `w-full px-3 py-2 border rounded shadow-sm ${errors[fieldName] ? "border-red-500 focus:ring-red-500" : "border-gray-300 focus:ring-blue-500"}`;
    };

    return (
        <div className="min-h-screen bg-gray-100 flex items-center justify-center p-4">
            <div className="bg-white p-6 rounded shadow-md w-full max-w-2xl">
                <h1 className="text-lg font-bold mb-4">Add New Movie</h1>
                <form onSubmit={handleSubmit} className="space-y-4">
                    {/* Text Inputs */}
                    {formTextFields.map(({ label, name, type }) => (
                        <div key={name} className="form-group">
                            <label htmlFor={name}>{label}</label>

                            {type === "textarea" ? (
                                <textarea
                                    id={name}
                                    name={name}
                                    value={data[name] || ""}
                                    onChange={handleChange}
                                    className={getInputClassName(name)}
                                />
                            ) : (
                                <input
                                    id={name}
                                    type={type}
                                    name={name}
                                    value={data[name] || ""}
                                    onChange={handleChange}
                                    className={getInputClassName(name)}
                                />
                            )}

                            {errors[name] && <p className="text-red-500 text-sm">{errors[name]}</p>}
                        </div>
                    ))}

                    {/* Age Restriction Radio Buttons */}
                    <div>
                        <label className="block text-gray-700 mb-2">Age Restriction</label>
                        <div className="flex space-x-4">
                            {["G", "PG", "PG-13", "R", "NC-17"].map((rating) => (
                                <label key={rating} className={`cursor-pointer px-4 py-2 border rounded shadow-sm select-none ${data.ageRating === rating ? "bg-blue-500 text-white" : "bg-gray-200 text-gray-700"}`}>
                                    <input
                                        type="radio"
                                        name="ageRating"
                                        value={rating}
                                        onChange={handleChange}
                                        className="hidden"
                                    />
                                    {rating}
                                </label>
                            ))}
                        </div>
                        {errors.ageRating && <p className="text-red-500 text-sm mt-2">{errors.ageRating}</p>}
                    </div>

                    {/* Rental Period */}
                    <div className="grid grid-cols-2 gap-4">
                        <div>
                            <label className="block text-gray-700">Rental Period Start</label>
                            <input
                                type="date"
                                name="rentalPeriodStart"
                                value={data.rentalPeriodStart}
                                onChange={handleChange}
                                className={getInputClassName("rentalPeriodStart")}
                            />
                            {errors["rentalPeriodStart"] && <p className="text-red-500 text-sm mt-1">This field is required.</p>}
                        </div>
                        <div>
                            <label className="block text-gray-700">Rental Period End</label>
                            <input
                                type="date"
                                name="rentalPeriodEnd"
                                value={data.rentalPeriodEnd}
                                onChange={handleChange}
                                className={getInputClassName("rentalPeriodEnd")}
                            />
                            {errors["rentalPeriodEnd"] &&
                                <p className="text-red-500 text-sm mt-1">{errors["rentalPeriodEnd"]}</p>}
                        </div>
                    </div>

                    {/* Movie Image Upload */}
                    <div>
                        <label className="block text-gray-700">Movie Image</label>
                        <UploadFilesZone onFileChange={handleFileChange} />
                        {errors["image"] && <p className="text-red-500 text-sm mt-1">{errors["image"]}</p>}
                    </div>

                    {/* Submit Button */}
                    <button
                        type="submit"
                        className="bg-blue-500 text-white px-4 py-2 rounded shadow hover:bg-blue-600 w-full"
                    >
                        Add Movie
                    </button>
                </form>
            </div>
        </div>
    );
};

export default ManageMoviePage;
