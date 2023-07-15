import React from 'react'
import Header from '../components/Header';
import { List } from 'react-native-paper';

export default function HomeScreen({ navigation }) {
  const onChamadoPressed = () => {
    navigation.reset({
      index: 0,
      routes: [{ name: 'ChamadosScreen' }],
    })
  }

  return (
    <>
      <Header title="SAT Mobile" />

      <List.Section>
        <List.Subheader>Menu Principal</List.Subheader>

        <List.Item title="Meus Chamados" description="Chamados atribuídos ao seu usuário"
          onPress={onChamadoPressed}
          left={() => <List.Icon icon="folder" />} />

        <List.Item title="Relatórios" description="Dashboards de performances"
          left={() => <List.Icon icon="folder" />} />
      </List.Section>
    </>
  )
}
