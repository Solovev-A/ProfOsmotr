import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { BarChart, Bar, Cell, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, Brush } from 'recharts';

import useStore from './../../hooks/useStore';
import Spinner from './../../components/spinner';

const StatisticsPage = () => {
    const { statisticsStore } = useStore();
    const { isLoading, examinationsData, loadExaminationsStatistics } = statisticsStore;

    useEffect(() => {
        loadExaminationsStatistics();
    }, [])

    if (isLoading) return <Spinner />

    return (
        <>
            <h2>Статистика медосмотров работников</h2>
            {
                examinationsData.length === 0
                    ? <p className="font-italic text-center">Нет ни одного медосмотра</p>
                    : <div style={{ height: "450px" }}>
                        <ResponsiveContainer width="100%" height="100%">
                            <BarChart
                                width={300}
                                height={300}
                                data={examinationsData}
                                margin={{
                                    top: 20,
                                    right: 30,
                                    left: 20,
                                    bottom: 5,
                                }}
                            >
                                <CartesianGrid strokeDasharray="3 3" />
                                <XAxis dataKey="period" />
                                <YAxis />
                                <Tooltip />
                                <Legend />
                                <Brush
                                    dataKey="period"
                                    height={30}
                                    stroke="#6c757d"
                                    startIndex={examinationsData.length > 12 ? examinationsData.length - 12 : 0}
                                />
                                <Bar
                                    dataKey="preliminaryExaminationsCount"
                                    stackId="a"
                                    fill="#007bff"
                                    name="Предварительные"
                                />
                                <Bar
                                    dataKey="contingentCheckupStatusesCount"
                                    stackId="a"
                                    fill="#ffc107"
                                    name="Периодические"
                                />
                            </BarChart>
                        </ResponsiveContainer>
                    </div>
            }

        </>
    );
}

export default observer(StatisticsPage);