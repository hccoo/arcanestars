package com.arcanestars.apigateway.filters

import org.springframework.cloud.gateway.filter.GatewayFilter
import org.springframework.cloud.gateway.filter.factory.AbstractGatewayFilterFactory
import org.springframework.stereotype.Component

@Component
class AuthGatewayFilterFactory : AbstractGatewayFilterFactory<Any>() {
    override fun apply(config: Any): GatewayFilter {
        return AuthGatewayFilter()
    }
}