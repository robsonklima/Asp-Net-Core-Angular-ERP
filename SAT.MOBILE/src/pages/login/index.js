import React, { useState, useEffect } from 'react';
import {
    KeyboardAvoidingView,
    View,
    Text,
    TextInput,
    TouchableOpacity,
    Animated,
    Keyboard
} from 'react-native';

import styles from './styles';

export default function App() {
    const [offset] = useState(new Animated.ValueXY({ x: 0, y: 80 }));
    const [opacity] = useState(new Animated.Value(0));
    const [logo] = useState(new Animated.ValueXY({ x: 170, y: 195 }));

    useEffect(() => {
        keyboardDidShowListener
            = Keyboard.addListener('keyboardDidShow', keyboardDidShow);

        keyboardDidHideListener
            = Keyboard.addListener('keyboardDidHide', keyboardDidHide);

        Animated.parallel([
            Animated.spring(offset.y, {
                toValue: 0,
                speed: 4,
                bounciness: 20
            }),

            Animated.timing(opacity, {
                toValue: 1,
                duration: 200
            })
        ]).start();
    }, []);

    function keyboardDidShow() {
        Animated.parallel([
            Animated.timing(logo.x, {
                toValue: 95,
                duration: 100
            }),

            Animated.timing(logo.y, {
                toValue: 105,
                duration: 100
            })
        ]).start();
    }

    function keyboardDidHide() {
        Animated.parallel([
            Animated.timing(logo.x, {
                toValue: 170,
                duration: 100
            }),

            Animated.timing(logo.y, {
                toValue: 195,
                duration: 100
            })
        ]).start();
    };

    return (
        <>
            <KeyboardAvoidingView style={styles.container}>
                <View style={styles.containerLogo}>
                    <Animated.Image
                        style={{
                            width: logo.x,
                            height: logo.y
                        }}
                        source={require('../../assets/images/logo.png')}
                    />
                </View>

                <Animated.View style={[
                    styles.form,
                    {
                        opacity: opacity,
                        transform: [
                            {
                                translateY: offset.y
                            }
                        ]
                    }
                ]}>
                    <TextInput
                        style={styles.input}
                        placeholder="Usuário"
                        textContentType="text"
                        autoCapitalize="none"
                        autoCorrect={false}
                        onChangeText={() => { }}
                    />

                    <TextInput
                        style={styles.input}
                        placeholder="Senha"
                        textContentType="password"
                        autoCapitalize="none"
                        autoCompleteType="password"
                        autoCorrect={false}
                        secureTextEntry={true}
                        onChangeText={() => { }}
                    />

                    <TouchableOpacity style={styles.buttonSubmit}>
                        <Text style={styles.submitText}>Entrar</Text>
                    </TouchableOpacity>

                    <TouchableOpacity style={styles.buttonForgotPass}>
                        <Text style={styles.forgotPassText}>Esquecí minha senha</Text>
                    </TouchableOpacity>
                </Animated.View>
            </KeyboardAvoidingView>
        </>
    );
};