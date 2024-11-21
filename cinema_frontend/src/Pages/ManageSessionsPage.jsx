import { useState } from "react";
import DatePickerModal from "../Components/DatePickerModal";
import FilmContainer from "../Components/FilmContainer.jsx";

const ManageSessionsPage = () => {
    const [isModalOpen, setIsModalOpen] = useState(true);
    const [interval, setInterval] = useState({
        startDate: null,
        endDate: null,
    });

    const handleConfirm = () => {
        if (interval.startDate && interval.endDate) {
            setIsModalOpen(false);
        } else {
            alert("Please select both start and end dates.");
        }
    };

    const handleChangeDates = () => {
        setIsModalOpen(true);
    };

    return (
        <div className="min-h-screen bg-gray-100 flex items-center justify-center relative">
            {!isModalOpen && (
                <div
                    className="absolute top-4 left-4 bg-blue-600 text-white px-4 py-2 rounded cursor-pointer shadow-md"
                    onClick={handleChangeDates}
                >
                    {`${interval.startDate?.toLocaleDateString()} - ${interval.endDate?.toLocaleDateString()}`}
                </div>
            )}

            <DatePickerModal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                interval={interval}
                setInterval={setInterval}
                onConfirm={handleConfirm}
            />

            <FilmContainer />
        </div>
    );
};

export default ManageSessionsPage;
