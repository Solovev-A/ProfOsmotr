import React, { Component } from 'react';
import ErrorMessage from './errorMessage';

class ErrorBoundary extends Component {
    state = {
        error: null
    }

    componentDidCatch(error) {
        this.setState({ error })
    }

    render() {
        const { error } = this.state;

        if (error || error === '') {
            return <ErrorMessage error={error} />
        }

        return this.props.children;
    }
}

export default ErrorBoundary;