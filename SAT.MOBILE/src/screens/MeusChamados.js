import React from 'react'
import { Text } from 'react-native-paper'

export default function MeusChamadosScreen({ navigation }) {
    const [rows, setRows] = useState({ value: [], error: '' })

    return (
        <View style={styles.container}>
            <FlatList
                data={[
                    { key: 'Devin' },
                    { key: 'Dan' },
                    { key: 'Dominic' },
                    { key: 'Jackson' },
                    { key: 'James' },
                    { key: 'Joel' },
                    { key: 'John' },
                    { key: 'Jillian' },
                    { key: 'Jimmy' },
                    { key: 'Julie' },
                ]}
                renderItem={({ item }) => <Text style={styles.item}>{item.key}</Text>}
            />
        </View>
    );
}