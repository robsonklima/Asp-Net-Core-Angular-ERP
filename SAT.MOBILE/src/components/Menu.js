import React from 'react'
import { List } from 'react-native-paper';

export default function Menu() {
    return <List.Section>
        <List.Subheader>Menu Principal</List.Subheader>

        <List.Item title="Meus Chamados" description="Chamados atribuídos ao seu usuário"
            left={() => <List.Icon icon="folder" />} />

        <List.Item title="Relatórios" description="Dashboards de performances"
            left={() => <List.Icon icon="folder" />} />
    </List.Section>
}