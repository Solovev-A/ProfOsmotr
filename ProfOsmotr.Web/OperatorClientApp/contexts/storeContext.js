import { createContext } from 'react';
import PreliminaryExaminationsStore from '../stores/preliminaryExaminationsStore';


const store = new PreliminaryExaminationsStore();
const StoreContext = createContext(store);

export default StoreContext;