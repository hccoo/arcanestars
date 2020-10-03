package com.arcanestars.apigateway.filters

import com.nimbusds.jose.JWSAlgorithm
import com.nimbusds.jose.jwk.source.JWKSource
import com.nimbusds.jose.jwk.source.RemoteJWKSet
import com.nimbusds.jose.proc.JWSKeySelector
import com.nimbusds.jose.proc.JWSVerificationKeySelector
import com.nimbusds.jose.proc.SecurityContext
import com.nimbusds.jwt.JWTClaimsSet
import com.nimbusds.jwt.proc.ConfigurableJWTProcessor
import com.nimbusds.jwt.proc.DefaultJWTProcessor
import net.minidev.json.JSONObject
import org.springframework.cloud.gateway.filter.GatewayFilter
import org.springframework.cloud.gateway.filter.GatewayFilterChain
import org.springframework.http.HttpStatus
import org.springframework.web.server.ServerWebExchange
import reactor.core.publisher.Mono
import java.net.URL
import java.text.ParseException
import org.springframework.core.Ordered as Ordered

class AuthGatewayFilter: GatewayFilter, Ordered {

    override fun filter(exchange: ServerWebExchange?, chain: GatewayFilterChain?): Mono<Void> {

        var authorized = false;

        var accessToken = exchange!!.request.headers.getFirst("Authorization");
        if(accessToken==null||accessToken==""){
            exchange!!.response.setStatusCode(HttpStatus.BAD_GATEWAY);
            return exchange.response.setComplete();
        }

        var jwkEndpoint = "http://localhost:5000/.well-known/openid-configuration/jwks";
        var token = processToken(accessToken);

        var jwtProcessor:ConfigurableJWTProcessor<SecurityContext> = DefaultJWTProcessor();
        //提供公钥地址来获取
        var keySource = RemoteJWKSet<SecurityContext>(URL(jwkEndpoint));
        //提供解析算法，算法类型要写对，服务器用的是什么就是什么，目前是RSA256算法
        var expectedJWSAlg:JWSAlgorithm = JWSAlgorithm.RS256;
        //填写 RSA 公钥来源从提供公钥地址获取那边得到
        var keySelector:JWSKeySelector<SecurityContext> = JWSVerificationKeySelector(expectedJWSAlg, keySource);

        if(keySelector==null){
            exchange!!.response.setStatusCode(HttpStatus.UNAUTHORIZED);
            return exchange.response.setComplete();
        }

        jwtProcessor.setJWSKeySelector(keySelector);
        //处理收到的token（令牌),错误则返回对象
        var ctx:SecurityContext? = null;
        var claimsSet: JWTClaimsSet? = null;
        try {
            claimsSet = jwtProcessor.process(token, ctx);
            authorized = true;
        } catch (e: Exception) {
            exchange.response.setStatusCode(HttpStatus.UNAUTHORIZED);
            e.printStackTrace();
            return exchange.response.setComplete();
        }
        //调试用，打印出来
        System.out.println(claimsSet.toJSONObject());
        //失败返回无授权
        if(claimsSet==null) {
            exchange.response.setStatusCode(HttpStatus.UNAUTHORIZED);
            return exchange.response.setComplete();
        }
        //解码里面具体内容，尤其角色，虽然这里不需要,顺利取出
        var jo: JSONObject = JSONObject(claimsSet.toJSONObject());
        //String role = jo.getString("role");

        if(authorized)
            return chain!!.filter(exchange)
        else
        {
            exchange!!.response.setStatusCode(HttpStatus.UNAUTHORIZED);
            return exchange.response.setComplete();
        }
    }

    override fun getOrder(): Int {
        return 10;
    }

    fun processToken(accessToken:String):String{
        var temp = accessToken.split(" ");
        return temp[1];
    }
    /*
    @Override
    Mono<Void> filter(ServerWebExchange exchange, GatewayFilterChain chain)
    {
        //获取header的参数
        String name = exchange.getRequest().getHeaders().getFirst("name");
        String password = exchange.getRequest().getHeaders().getFirst("password");
        boolean permitted = AuthUtil.isPermitted(name, password);//权限比较
        if (permitted)
        {
            return chain.filter(exchange);
        }
        else
        {
            exchange.getResponse().setStatusCode(HttpStatus.UNAUTHORIZED);
            return exchange.getResponse().setComplete();
        }
    }

    @Override
    public int getOrder()
    {
        return 10;
    }*/
}