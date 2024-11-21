import React, { useCallback, useState } from 'react';
import { useDropzone } from 'react-dropzone';

const UploadFileZone = ({ onFileChange }) => {
    const [file, setFile] = useState(null);

    const onDrop = useCallback((acceptedFiles) => {
        const uploadedFile = acceptedFiles[0];
        const fileData = {
            file: uploadedFile,
            preview: URL.createObjectURL(uploadedFile),
        };
        setFile(fileData);

        // Trigger the onFileChange callback to notify parent component
        onFileChange(fileData);
    }, [onFileChange]);

    const removeFile = () => {
        setFile(null);
        onFileChange(null); // Notify parent component that file has been removed
    };

    const { getRootProps, getInputProps, isDragActive } = useDropzone({
        onDrop,
        accept: {
            'image/*': ['.jpeg', '.jpg', '.png'],
        },
        multiple: false,
    });

    return (
        <div className="p-4 border border-gray-300 rounded">
            <div
                {...getRootProps()}
                className={`p-6 text-center border-2 border-dashed rounded cursor-pointer ${
                    isDragActive ? 'border-blue-400 bg-blue-50' : 'border-gray-300'
                }`}
            >
                <input {...getInputProps()} />
                {isDragActive ? (
                    <p className="text-blue-600">Drop the file here...</p>
                ) : (
                    <p className="text-gray-500">
                        Drag and drop a file here, or click to select one.
                    </p>
                )}
            </div>

            {file && (
                <div className="mt-4">
                    <h3 className="mb-2 text-lg font-semibold">Uploaded File</h3>
                    <div className="flex items-center justify-between p-2 border rounded">
                        <div className="flex items-center space-x-4">
                            <img
                                src={file.preview}
                                alt={file.file.name}
                                className="w-10 h-10 rounded"
                            />
                            <span className="text-gray-700">{file.file.name}</span>
                        </div>
                        <button
                            onClick={removeFile}
                            className="px-2 py-1 text-white bg-red-500 rounded"
                        >
                            Remove
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
};

export default UploadFileZone;