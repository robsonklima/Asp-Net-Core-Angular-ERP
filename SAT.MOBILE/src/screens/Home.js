import React from 'react'
import Background from '../components/Background'
import Logo from '../components/Logo'
import Header from '../components/Header'
import Paragraph from '../components/Paragraph'
import Button from '../components/Button'

export default function Home({ navigation }) {
  return (
    <Background>
      <Logo />

      <Header>Olá</Header>

      <Paragraph>
        Bem-vindo de volta ao aplicativo do SAT.
      </Paragraph>

      <Button
        mode="outlined"
        onPress={() =>
          navigation.reset({
            index: 0,
            routes: [{ name: 'LoginScreen' }],
          })
        }
      >
        Sair
      </Button>
    </Background>
  )
}
