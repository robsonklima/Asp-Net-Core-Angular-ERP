import React from 'react'
import Header from '../components/Header';
import BackButton from '../components/BackButton';
import { List } from 'react-native-paper'

export default function ChamadosScreen({ navigation }) {
    const onChamadoPressed = () => {
        navigation.reset({
            index: 0,
            routes: [{ name: 'HomeScreen' }],
        })
    }

    return (
        <>
            <Header />

            <List.Section>
                <List.Subheader>
                    Meus Chamados
                </List.Subheader>

                <List.Item
                    title="7896131"
                    description="Agência Sicredi, Rua Dorival Cândido Silva 511, Porto Alegre"
                    left={() => <List.Icon icon="ticket" />}
                    onPress={(data) => { onChamadoPressed }}
                />
                <List.Item
                    title="7899563"
                    description="Agência Banco do Brasil, Avenida Mauá 2411, Porto Alegre"
                    left={() => <List.Icon icon="ticket" />}
                    onPress={(data) => { console.log('Clicked'); }}
                />
                <List.Item
                    title="7899844"
                    description="Agência Banrisul, Avenida Flores da cunha 3219, Cachoeirinha"
                    left={() => <List.Icon icon="ticket" />}
                    onPress={(data) => { console.log('Clicked'); }}
                />
            </List.Section>
        </>
    )
}
