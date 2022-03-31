import { useEffect } from 'react';

import useStore from "./useStore"

const useICD10Store = () => {
    const { icd10Store } = useStore();

    useEffect(() => {
        if (!icd10Store.isLoaded) {
            icd10Store.load();
        }
    }, [])

    return icd10Store;
}

export default useICD10Store;