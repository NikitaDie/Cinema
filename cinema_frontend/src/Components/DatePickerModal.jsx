import React from "react";
import Datepicker from "react-tailwindcss-datepicker";
import DATEPICKER_CONFIGS from "../configs/DATEPICKER_CONFIGS.js";

const DatePickerModal = ({
    isOpen,
    onClose,
    interval,
    setInterval,
    onConfirm
}) => {
    const MIN_DATE = new Date();
    MIN_DATE.setDate(MIN_DATE.getDate());

    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <div className="bg-white rounded-lg p-6 w-96">
                <h2 className="text-xl font-semibold mb-4 text-center">
                    Select Date Range
                </h2>
                <div className="flex flex-col space-y-4">
                    <Datepicker
                        value={interval}
                        primaryColor="purple"
                        displayFormat="DD/MM/YYYY"
                        showShortcuts={true}
                        minDate={MIN_DATE}
                        separator="to"
                        startWeekOn="mon"
                        showFooter={true}
                        required={true}
                        configs={DATEPICKER_CONFIGS}
                        onChange={(newInterval) => setInterval(newInterval)}
                    />
                    <button
                        className="bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700"
                        onClick={onConfirm}
                    >
                        Confirm
                    </button>
                </div>
            </div>
        </div>
    );
};

export default DatePickerModal;
