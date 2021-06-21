import { useState } from "react";

const useErrorHandler = () => {
    const [, setState] = useState(null);

    return (error) => {
        setState(() => {
            throw error;
        });
    };
}

export default useErrorHandler;