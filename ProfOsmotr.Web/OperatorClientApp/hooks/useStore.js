import { useContext } from 'react';
import StoreContext from '../contexts/storeContext';

const useStore = () => useContext(StoreContext);

export default useStore;