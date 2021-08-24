import { useEffect } from 'react';

import useErrorHandler from './useErrorHandler';

const useListPage = (listStore) => {
    const errorHandler = useErrorHandler();

    useEffect(() => {
        listStore.loadInitialList()
            .catch(errorHandler);

        return () => {
            listStore.reset();
        }
    }, []);
}

export default useListPage;