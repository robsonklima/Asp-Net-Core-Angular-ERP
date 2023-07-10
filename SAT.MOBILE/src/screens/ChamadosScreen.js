import React, { useEffect, useState } from 'react';
import { View, Text, FlatList } from 'react-native';
const globals = require('../models/Globals');

const ChamadosScreen = props => {
    const [isLoading, setLoading] = useState(false);
    const [chamados, setchamados] = useState([]);

    getChamados = () => {
        fetch(`${globals.BASE_URL}/OrdemServico?codTecnico=${usuario.codTecnico}&pageSize=10`)
            .then((response) => response.json())
            .then((json) => setchamados(json))
            .catch((error) => console.error(error))
            .finally(() => setLoading(false));
    }

    useEffect(() => {
        setLoading(true);
        getChamados();
    }, []);

    return (
        <View style={{ padding: 20 }}>
            {isLoading ? <Text>Loading...</Text> :
                (
                    <FlatList
                        data={chamados}
                        keyExtractor={({ id }) => id.toString()}
                        renderItem={({ os }) => <Text>{os.codOS}  </Text>}
                    />
                )}
        </View>
    );
};
export default ChamadosScreen;