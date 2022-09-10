using System;

namespace PixelMCShowdownAPI.ELO
{
    class ELOPlayer
    {
        public string name;

        public int place = 0;
        public int eloPre = 0;
        public int eloPost = 0;
        public int eloChange = 0;
    }

    class ELOMatch
    {
        private List<ELOPlayer> players = new List<ELOPlayer>();
        private float _kFactor;
        private int _eloCap;

        public ELOMatch(float K, int eloCap)
        {
            this._kFactor = K;
            this._eloCap = eloCap;
        }

        public void AddPlayer(string name, int place, int elo)
        {
            ELOPlayer player = new ELOPlayer();

            player.name = name;
            player.place = place;
            player.eloPre = elo;

            players.Add(player);
        }

        public int GetELO(string name)
        {
            foreach (ELOPlayer p in players)
            {
                if (p.name == name)
                    return p.eloPost;
            }
            return -1;
        }

        public int GetELOChange(string name)
        {
            foreach (ELOPlayer p in players)
            {
                if (p.name == name)
                    return p.eloChange;
            }
            return 0;
        }

        public void CalculateELOs()
        {
            int n = players.Count;
            float K = _kFactor / (float)(n - 1);

            for (int i = 0; i < n; i++)
            {
                int curPlace = players[i].place;
                int curELO = players[i].eloPre;

                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        int opponentPlace = players[j].place;
                        int opponentELO = players[j].eloPre;

                        //work out S
                        float S;
                        if (curPlace < opponentPlace)
                            S = 1.0F;
                        else if (curPlace == opponentPlace)
                            S = 0.5F;
                        else
                            S = 0.0F;

                        //work out EA
                        float EA = 1 / (1.0f + (float)Math.Pow(10.0f, (opponentELO - curELO) / 400.0f));

                        //calculate ELO change vs this one opponent, add it to our change bucket
                        //I currently round at this point, this keeps rounding changes symetrical between EA and EB, but changes K more than it should
                        int eloChange = (int)Math.Round(K * (S - EA));
                        if (eloChange > this._eloCap)
                            eloChange = this._eloCap;
                        else if (eloChange < (-this._eloCap))
                            eloChange = -this._eloCap;
                        players[i].eloChange += eloChange;
                    }
                }
                //add accumulated change to initial ELO for final ELO   
                players[i].eloPost = players[i].eloPre + players[i].eloChange;
            }
        }
    }
}
