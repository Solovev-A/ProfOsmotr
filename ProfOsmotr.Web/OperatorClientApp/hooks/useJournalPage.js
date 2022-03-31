import { useEffect } from 'react';
import useListPage from './useListPage';


const useJournalPage = (examinationsStore) => {
    useListPage(examinationsStore.journal);
    useEffect(() => {
        return () => {
            examinationsStore.resetJournal();
        }
    }, [])
}

export default useJournalPage;