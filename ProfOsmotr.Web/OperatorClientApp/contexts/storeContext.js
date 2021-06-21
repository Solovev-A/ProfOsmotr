import { createContext } from 'react';
import RootStore from './../stores/rootStore';

const store = new RootStore();
const StoreContext = createContext(store);

export default StoreContext;