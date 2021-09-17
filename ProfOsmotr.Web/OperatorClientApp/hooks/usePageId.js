import { useEffect } from 'react';
import { useParams } from 'react-router';

import useErrorHandler from './useErrorHandler';
import CancellationToken from '../utils/cancellationToken';

const usePageId = ({ loader, slugSetter, onReset }) => {
    const { id } = useParams();

    const errorHandler = useErrorHandler();

    useEffect(() => {
        if (slugSetter && typeof slugSetter === 'function') {
            slugSetter(id);
        }

        const cancellationToken = new CancellationToken();
        if (loader && typeof loader === 'function') {
            loader(cancellationToken)
                .catch(errorHandler);
        }

        return () => {
            cancellationToken.cancel();
            if (onReset && typeof onReset === 'function') {
                onReset();
            }
        }
    }, [id]);
}

export default usePageId;