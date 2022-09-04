package co.pixelmc.listeners;

import co.pixelmc.models.WebApiPostPlayer;
import co.pixelmc.services.PlayerService;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import net.minecraft.entity.player.ServerPlayerEntity;
import net.minecraftforge.event.entity.player.PlayerEvent;
import net.minecraftforge.eventbus.api.Event;
import net.minecraftforge.eventbus.api.SubscribeEvent;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

import java.io.IOException;
import java.util.Arrays;
import java.util.Collections;
import org.apache.logging.log4j.Logger;

public class PlayerLoginListener {
    private final PlayerService playerService;
    private final Logger logger;

    public PlayerLoginListener(PlayerService playerService, Logger logger){
        this.playerService = playerService;
        this.logger = logger;
    }

    @SubscribeEvent
    public void onPlayerLogin(PlayerEvent.PlayerLoggedInEvent event){
        ServerPlayerEntity e = (ServerPlayerEntity) event.getPlayer();
        WebApiPostPlayer webApiPostPlayer = new WebApiPostPlayer();
        webApiPostPlayer.setPlayerName(e.getName().getContents());
        webApiPostPlayer.setUuid(e.getUUID());

        try {
            playerService.addPlayers(Collections.singletonList(webApiPostPlayer));
            logger.info("Successfully added " + e.getUUID() + " to webapi");
        } catch (IOException ex) {
            logger.error(ex.getStackTrace());
        }
    }
}
